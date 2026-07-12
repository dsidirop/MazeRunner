#!/bin/bash

# declare -r NUGET_FEED_URL="$1"
# declare -r NUGET_FEED_USERNAME="$2"
# declare -r NUGET_FEED_ACCESSTOKEN="$3"

declare -r ARTIFACTS_FOLDER_PATH="$4"

#if [ -z "${NUGET_FEED_URL}" ]; then
#  echo "##vso[task.logissue type=error]Missing 'NUGET_FEED_URL' which was expected to be parameter #1."
#  exit 1
#fi

#if [ -z "${NUGET_FEED_USERNAME}" ]; then
#  echo "##vso[task.logissue type=error]Missing 'NUGET_FEED_USERNAME' which was expected to be parameter #2."
#  exit 2
#fi

#if [ -z "${NUGET_FEED_ACCESSTOKEN}" ]; then
#  echo "##vso[task.logissue type=error]Missing 'NUGET_FEED_ACCESSTOKEN' which was expected to be parameter #3."
#  exit 3
#fi

if [ -z "${ARTIFACTS_FOLDER_PATH}" ]; then
  echo "##vso[task.logissue type=error]Missing 'ARTIFACTS_FOLDER_PATH' which was expected to be parameter #4."
  exit 4
fi

echo
echo "** Dotnet CLI:"
which    dotnet   &&   dotnet   --version
declare exitCode=$?
if [ $exitCode != 0 ]; then
  echo "##vso[task.logissue type=error]Something's wrong with 'dotnet' cli."
  exit 50
fi

echo -e "Checking for .NET 11 SDK installation..."
if dotnet --list-sdks | grep -q "11.0"; then
    echo " .NET 11 SDK is already installed"
else
    echo " .NET11 SDK is not installed - installing it now..."
    
    curl -L -o "dotnet-install.ps1" "https://dot.net/v1/dotnet-install.ps1" # Use the dotnet-install script which works better in bash
    declare exitCode=$?
    if [ $exitCode != 0 ]; then
      echo "##vso[task.logissue type=error]Failed to download the dotnet11 installation script."
      exit 60
    fi
    
    export DOTNET_INSTALL_SKIP_TAR=1 # forces dotnet-install.ps1 to use .zip instead of .tar.gz  needed because on this runner 'tar' resolves to git-bash's MSYS2 tar (via the inherited PATH) which cannot handle windows drive-letter paths (eg 'C:\...') and fails with "Cannot connect to C: resolve failed"

    pwsh -Command "./dotnet-install.ps1 -Channel 11.0 -InstallDir 'C:\Program Files\dotnet'"
    declare exitCode=$?
    if [ $exitCode != 0 ]; then
      echo "##vso[task.logissue type=error]Failed to install dotnet11."
      exit 61
    fi

    if [ -f "$HOME/.bashrc" ]; then # update path permanently in bash profile
      if ! grep -q "Program Files/dotnet" "$HOME/.bashrc"; then
        echo 'export PATH="/c/Program Files/dotnet:$PATH"' >> "$HOME/.bashrc"
        echo "Added .NET to PATH in .bashrc"
      fi
    fi
    
    if [ -f "$HOME/.bash_profile" ]; then # also update .bash_profile if it exists
      if ! grep -q "Program Files/dotnet" "$HOME/.bash_profile"; then
        echo 'export PATH="/c/Program Files/dotnet:$PATH"' >> "$HOME/.bash_profile"
        echo "Added .NET to PATH in .bash_profile"
      fi
    fi
    
    export PATH="/c/Program Files/dotnet:$PATH" # update path to include the new installation if needed
    
    dotnet --version # verify installation
    declare exitCode=$?
    if [ $exitCode != 0 ]; then
      echo "##vso[task.logissue type=error]Failed to install dotnet11."
      exit 65
    fi
fi

cd "Code" #vital
declare exitCode=$?
if [ $exitCode != 0 ]; then
  echo "##vso[task.logissue type=error]Failed to cd to 'Code' folder."
  exit 65
fi
dotnet                                       \
             workload                        \
             restore
declare exitCode=$?
if [ $exitCode != 0 ]; then
  echo "##vso[task.logissue type=error]Failed to restore dotnet workloads."
  exit 70
fi
# cd - || exit 71 #nah    better stay inside the 'Code' folder for the next steps

echo
echo "** Adding 'Artifacts' Folder as a Nuget Source (dotnet):"
mkdir -p "${ARTIFACTS_FOLDER_PATH}"   &&   dotnet   nuget   add   source   "${ARTIFACTS_FOLDER_PATH}"   --name "LocalArtifacts"
declare exitCode=$?
if [ $exitCode != 0 ]; then
  echo "##vso[task.logissue type=error]Failed to add 'Artifacts' folder as a nuget source."
  exit 170
fi

#echo
#echo "** Adding 'Private Nuget Feed' as a Nuget Source:"
## keep this after workload-restoration   otherwise we will run into problems    note that the 'store-password-in-clear-text'
## is necessary for azure pipelines   once we move fully over to github actions we can remove this parameter completely
#dotnet   nuget   add                                     \
#    source      "${NUGET_FEED_URL}"                      \
#    --name      "Private"                                \
#    --username  "${NUGET_FEED_USERNAME}"                 \
#    --password  "${NUGET_FEED_ACCESSTOKEN}"              \
#    --store-password-in-clear-text
#declare exitCode=$?
#if [ $exitCode != 0 ]; then
#  echo "##vso[task.logissue type=error]Failed to add 'MazeRunner Nuget Feed' as a nuget source."
#  exit 180
#fi

dotnet nuget list source
