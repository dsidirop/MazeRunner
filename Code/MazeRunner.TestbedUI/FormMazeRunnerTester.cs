using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MazeRunner.Mazes;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;

namespace MazeRunner.TestbedUI
{
    public partial class FormMazeRunnerTester : Form
    {
        private readonly IMazesFactory _mazesFactory;
        private readonly IEnginesFactory _enginesFactory;
        private readonly IEnginesTestbench _enginesTestbench;

        public FormMazeRunnerTester(IEnginesFactory enginesFactory, IMazesFactory mazesFactory, IEnginesTestbench enginesTestbench)
        {
            InitializeComponent();

            _mazesFactory = mazesFactory;
            _enginesFactory = enginesFactory;
            _enginesTestbench = enginesTestbench;
        }

        protected override void OnLoad(EventArgs ea)
        {
            lbxkAvailableEngines.ValueMember = nameof(EngineEntry.Selected); //order
            lbxkAvailableEngines.DisplayMember = nameof(EngineEntry.Name); //order
            lbxkAvailableEngines.DataSource = new BindingList<EngineEntry>(_enginesFactory.EnginesNames.Select(x => new EngineEntry {Selected = true, Name = x}).ToList()); //order

            (lbxkAvailableEngines.DataSource as BindingList<EngineEntry>).Each((x, i) => lbxkAvailableEngines.SetItemChecked(i, x.Selected)); //order
        }

        [Obfuscation(Exclude = true, ApplyToMembers = true)] //0
        private sealed class EngineEntry
        {
            public string Name { get; set; }
            public bool Selected { get; set; }
        }
        //0 we could have reduce this to [Obfuscation] but it wouldnt be that clear what we are after in terms of obfuscation
    }
}
