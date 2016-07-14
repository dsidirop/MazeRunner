using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MazeRunner.Mazes;
using MazeRunner.Shared.Helpers;
using MazeRunner.Shared.Interfaces;
// ReSharper disable NotAccessedField.Local

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
            _enginesTestbench = enginesTestbench; // ReSharper disable once PossibleNullReferenceException
            lbxkEnginesToBenchmark.ItemCheck += (s, ea) => (lbxkEnginesToBenchmark.DataSource as BindingList<EngineEntry>)[ea.Index].Selected = ea.NewValue == CheckState.Checked;
        }

        protected override void OnShown(EventArgs ea)
        {
            _ccMazeCanvas.Maze = _mazesFactory.Random(width: 10, height: 6, roadblocksDensity: 0.3);

            lbxkEnginesToBenchmark.ValueMember = nameof(EngineEntry.Selected); //order
            lbxkEnginesToBenchmark.DisplayMember = nameof(EngineEntry.Name); //order
            lbxkEnginesToBenchmark.DataSource = new BindingList<EngineEntry>(_enginesFactory.EnginesNames.Select(x => new EngineEntry {Selected = true, Name = x}).ToList()); //order

            (lbxkEnginesToBenchmark.DataSource as BindingList<EngineEntry>).Each((x, i) => lbxkEnginesToBenchmark.SetItemChecked(i, x.Selected)); //order
        }

        [Obfuscation(Exclude = true, ApplyToMembers = true)] //0
        private sealed class EngineEntry
        {
            public string Name { get; set; }
            public bool Selected { get; set; }
        }
        //0 we could have reduce this to [Obfuscation] but it wouldnt be that clear what we are after in terms of obfuscation

        private void btnStart_Click(object sender, EventArgs ea)
        {
            _ccMazeCanvas.Maze = _mazesFactory.Random(width: 10, height: 10, roadblocksDensity: 0.4);
        }

        private void btnStop_Click(object sender, EventArgs ea)
        {
            _ccMazeCanvas.SetCustomColorOnCell(new Point(5, 5), Color.Gray);
        }
    }
}
