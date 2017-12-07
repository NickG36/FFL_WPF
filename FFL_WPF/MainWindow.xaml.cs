using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FFL_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ComboBox> all_cbs = new List<ComboBox>();
        List<String> all_teams_list = new List<String>();
        ReadOnlyCollection<String> all_teams;
        List<String> remaining_teams = new List<String>();
        Fixtures fixtures = new ExcelFixtures();
        Results results = new ExcelResults();
        ResultsSummary res_summary;
        public MainWindow()
        {
            InitializeComponent();
            all_cbs.Add(homeCB1);
            all_cbs.Add(awayCB1);
            all_cbs.Add(homeCB2);
            all_cbs.Add(awayCB2);
            all_cbs.Add(homeCB3);
            all_cbs.Add(awayCB3);
            all_cbs.Add(homeCB4);
            all_cbs.Add(awayCB4);
            all_cbs.Add(homeCB5);
            all_cbs.Add(awayCB5);
            all_cbs.Add(homeCB6);
            all_cbs.Add(awayCB6);
            all_cbs.Add(homeCB7);
            all_cbs.Add(awayCB7);
            all_cbs.Add(homeCB8);
            all_cbs.Add(awayCB8);
            all_cbs.Add(homeCB9);
            all_cbs.Add(awayCB9);
            all_cbs.Add(homeCB10);
            all_cbs.Add(awayCB10);

            foreach (CommonTypes.TeamName team_nm in
                      Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                all_teams_list.Add(GenUtils.ToLongString(team_nm));
            }

            all_teams = new ReadOnlyCollection<String>(all_teams_list);
            resetCBs();
            updateFixturesPane(false);

            res_summary = new ResultsSummary(att_grid: attDataGrid,
                                             def_grid: defDataGrid);

            if (results.doesFileExist())
            {
                res_summary.reCalculate(results.getAllResults(),
                                        fixtures.readAllFixtures());
                updateExistingResults(false);
            }

            commitResBtn.IsEnabled = false;
        }

        /// <summary>
        /// Updates the existing results in the results pane.
        /// If no results file is found, user will be informed if parameter is true
        /// </summary>
        /// <param name="inform">in</param>
        private void updateExistingResults(bool inform)
        {
            if (!results.doesFileExist())
            {
                if (inform)
                {
                    MessageBox.Show($"Can't find results file. No results found",
                                    "Results file not found",
                                    MessageBoxButton.OK);
                }
            }
            else
            {
                existResTB.Text = "";

                var all_results_blocks = results.getAllResultsBlocks();

                string existing_res_str = "";

                foreach (var curr_block in all_results_blocks)
                {
                    existing_res_str += curr_block.week_description + "\n";

                    foreach (var curr_res in curr_block.results)
                    {
                        existing_res_str += curr_res + "\n";
                    }
                    existing_res_str += "___________________________\n";
                }

                existResTB.Text = existing_res_str;

            }
        }

        private void resetCBs()
        {
            remaining_teams = new List<String>(all_teams);

            foreach (ComboBox curr_cb in all_cbs)
            {
                curr_cb.Items.Clear();
                foreach (String curr_str in all_teams)
                {

                    curr_cb.Items.Add(curr_str);
                }
                curr_cb.Text = "";
            }

            enableCB(0);
        }

        private void onLoad(object sender, EventArgs e)
        {
            enableCB(0);
        }

        /// <summary>
        /// Enable the combo box with the given index and disable all others.
        /// If the index is too big then it will cope and not enable any combo box
        /// </summary>
        /// <param name="index"></param>
        private void enableCB(int index)
        {
            // Firstly, disable all combo boxes
            foreach (ComboBox curr_cb in all_cbs)
            {
                curr_cb.IsEnabled = false;
            }

            if (index < 20)
                all_cbs[index].IsEnabled = true;
        }

        private void valueChangedInCB(int CB_idx)
        {
            var sel_item = all_cbs[CB_idx - 1].SelectedItem;

            // Check that value has changed due to user selection rather than due
            // to automatically being reset
            if (sel_item != null)
            {
                string sel_string = sel_item.ToString();
                int idx = remaining_teams.IndexOf(sel_string);
                remaining_teams.RemoveAt(idx);

                if (CB_idx < GenUtils.NUM_TEAMS)
                    resetCB(CB_idx);
            }
        }

        private void resetCB(int idx)
        {
            ComboBox curr_cb = all_cbs[idx];
            curr_cb.Items.Clear();

            foreach (String curr_str in remaining_teams)
            {
                curr_cb.Items.Add(curr_str);
            }
            enableCB(idx);
        }


        // Give the option to hide all the valueChanged fns
        #region

        private void valueChanged1(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(1);
        }

        private void valueChanged2(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(2);
        }

        private void valueChanged3(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(3);
        }

        private void valueChanged4(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(4);
        }

        private void valueChanged5(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(5);
        }

        private void valueChanged6(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(6);
        }

        private void valueChanged7(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(7);
        }

        private void valueChanged8(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(8);
        }

        private void valueChanged9(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(9);
        }

        private void valueChanged10(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(10);
        }

        private void valueChanged11(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(11);
        }

        private void valueChanged12(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(12);
        }

        private void valueChanged13(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(13);
        }

        private void valueChanged14(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(14);
        }

        private void valueChanged15(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(15);
        }

        private void valueChanged16(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(16);
        }

        private void valueChanged17(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(17);
        }

        private void valueChanged18(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(18);
        }

        private void valueChanged19(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(19);
        }

        private void valueChanged20(object sender, SelectionChangedEventArgs e)
        {
            valueChangedInCB(20);
        }
        #endregion

        private void resetClick(object sender, RoutedEventArgs e)
        {
            resetCBs();
        }

        /// <summary>
        /// Updates the fixtures in fixtures pane.
        /// If no fixtures file is found, user will be informed if parameter is true
        /// </summary>
        /// <param name="inform">in</param>
        private void updateFixturesPane(bool inform)
        {
            if (!fixtures.doesFileExist())
            {
                if (inform)
                {
                    var path = fixtures.getPathToFile();

                    MessageBox.Show($"Can't find fixtures file at {path}. No fixtures found",
                                    "Fixtures file not found",
                                    MessageBoxButton.OK);
                }
            }
            else
            {
                List<Fixtures.FixturesBlock> all_fixtures = fixtures.readAllFixtureBlocks();

                string existing_fix_str = "";

                foreach (var curr_fixture_block in all_fixtures)
                {
                    existing_fix_str += curr_fixture_block.week_description + "\n";

                    foreach (var curr_fix in curr_fixture_block.fixtures)
                    {
                        existing_fix_str += curr_fix + "\n";
                    }
                    existing_fix_str += "__________________________\n";
                }

                existingFixScrTB.Text = existing_fix_str;
            }
        }


        private void updateFixPressed(object sender, RoutedEventArgs e)
        {
            updateFixturesPane(true);
        }

        private void commitFixBtnPressed(object sender, RoutedEventArgs e)
        {
            var week_name = weekTB.Text;

            var new_fixtures = new Fixtures.FixturesBlock();

            if (fixtures.fileExistsCanCreate())
            {
                new_fixtures.week_description = week_name;

                // Alternate CBs are home and away
                for (int cb_idx = 0; cb_idx < GenUtils.NUM_TEAMS / 2; ++cb_idx)
                {
                    var home_str = all_cbs[cb_idx * 2].Text;
                    var away_str = all_cbs[cb_idx * 2 + 1].Text;
                    var new_fixture = new CommonTypes.TwoTeams(home: home_str,
                                                               away: away_str);

                    new_fixtures.fixtures.Add(new_fixture);
                }
            }

            fixtures.addBlock(new_fixtures);

            resetCBs();
            weekTB.Text = "";
            // Update the pane showing fixtures
            updateFixturesPane(false);
            updateExistingResults(false);
            res_summary.reCalculate(results.getAllResults(),
                                    fixtures.readAllFixtures());
        }

        private bool validateScores()
        {
            bool result = true;
            try
            {
                Convert.ToInt16(homeScore1.Text);
                Convert.ToInt16(homeScore2.Text);
                Convert.ToInt16(homeScore3.Text);
                Convert.ToInt16(homeScore4.Text);
                Convert.ToInt16(homeScore5.Text);
                Convert.ToInt16(homeScore6.Text);
                Convert.ToInt16(homeScore7.Text);
                Convert.ToInt16(homeScore8.Text);
                Convert.ToInt16(homeScore9.Text);
                Convert.ToInt16(homeScore10.Text);

                Convert.ToInt16(awayScore1.Text);
                Convert.ToInt16(awayScore2.Text);
                Convert.ToInt16(awayScore3.Text);
                Convert.ToInt16(awayScore4.Text);
                Convert.ToInt16(awayScore5.Text);
                Convert.ToInt16(awayScore6.Text);
                Convert.ToInt16(awayScore7.Text);
                Convert.ToInt16(awayScore8.Text);
                Convert.ToInt16(awayScore9.Text);
                Convert.ToInt16(awayScore10.Text);

            }
            catch (System.FormatException)
            {
                result = false;
                MessageBox.Show("Error - score must be a number!");
            }

            return result;
        }
        private void commitResBtnClick(object sender, RoutedEventArgs e)
        {
            if (!validateScores())
                return;

            MessageBox.Show($"This will put these results in the Results.csv file, remove these fixtures from the Fixtures.csv file and update the Results panes.",
                            "Updating",
                            MessageBoxButton.OK);

            fixtures.deleteFirstBlock();
            updateFixturesPane(false);

            var res_block = new Results.ResultsBlock();

            res_block.week_description = (string)ResultsWkLbl.Content;

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.ToTeamName((string)homeLbl1.Content),
                                        away_team: GenUtils.ToTeamName((string)awayLbl1.Content),
                                        home_score: Convert.ToUInt16(homeScore1.Text),
                                        away_score: Convert.ToUInt16(awayScore1.Text)));

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.ToTeamName((string)homeLbl2.Content),
                                        away_team: GenUtils.ToTeamName((string)awayLbl2.Content),
                                        home_score: Convert.ToUInt16(homeScore2.Text),
                                        away_score: Convert.ToUInt16(awayScore2.Text)));

            res_block.results.Add(
                new CommonTypes.Result(home_team: GenUtils.ToTeamName((string)homeLbl3.Content),
                                       away_team: GenUtils.ToTeamName((string)awayLbl3.Content),
                                       home_score: Convert.ToUInt16(homeScore3.Text),
                                       away_score: Convert.ToUInt16(awayScore3.Text)));
            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.ToTeamName((string)homeLbl4.Content),
                                        away_team: GenUtils.ToTeamName((string)awayLbl4.Content),
                                        home_score: Convert.ToUInt16(homeScore4.Text),
                                        away_score: Convert.ToUInt16(awayScore4.Text)));

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.ToTeamName((string)homeLbl5.Content),
                                        away_team: GenUtils.ToTeamName((string)awayLbl5.Content),
                                        home_score: Convert.ToUInt16(homeScore5.Text),
                                        away_score: Convert.ToUInt16(awayScore5.Text)));

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.ToTeamName((string)homeLbl6.Content),
                                        away_team: GenUtils.ToTeamName((string)awayLbl6.Content),
                                        home_score: Convert.ToUInt16(homeScore6.Text),
                                        away_score: Convert.ToUInt16(awayScore6.Text)));

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.ToTeamName((string)homeLbl7.Content),
                                        away_team: GenUtils.ToTeamName((string)awayLbl7.Content),
                                        home_score: Convert.ToUInt16(homeScore7.Text),
                                        away_score: Convert.ToUInt16(awayScore7.Text)));

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.ToTeamName((string)homeLbl8.Content),
                                        away_team: GenUtils.ToTeamName((string)awayLbl8.Content),
                                        home_score: Convert.ToUInt16(homeScore8.Text),
                                        away_score: Convert.ToUInt16(awayScore8.Text)));

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.ToTeamName((string)homeLbl9.Content),
                                        away_team: GenUtils.ToTeamName((string)awayLbl9.Content),
                                        home_score: Convert.ToUInt16(homeScore9.Text),
                                        away_score: Convert.ToUInt16(awayScore9.Text)));

            res_block.results.Add(
                 new CommonTypes.Result(home_team: GenUtils.ToTeamName((string)homeLbl10.Content),
                                        away_team: GenUtils.ToTeamName((string)awayLbl10.Content),
                                        home_score: Convert.ToUInt16(homeScore10.Text),
                                        away_score: Convert.ToUInt16(awayScore10.Text)));

            results.addResultsBlock(res_block);
            updateExistingResults(false);

            res_summary.reCalculate(results.getAllResults(),
                                    fixtures.readAllFixtures());

            commitResBtn.IsEnabled = false;

            clearInputFields();

        }
        private void clearInputFields()
        {
            homeScore1.Text = "0"; awayScore1.Text = "0";
            homeScore2.Text = "0"; awayScore2.Text = "0";
            homeScore3.Text = "0"; awayScore3.Text = "0";
            homeScore4.Text = "0"; awayScore4.Text = "0";
            homeScore5.Text = "0"; awayScore5.Text = "0";
            homeScore6.Text = "0"; awayScore6.Text = "0";
            homeScore7.Text = "0"; awayScore7.Text = "0";
            homeScore8.Text = "0"; awayScore8.Text = "0";
            homeScore9.Text = "0"; awayScore9.Text = "0";
            homeScore10.Text = "0"; awayScore10.Text = "0";

            ResultsWkLbl.Content = "";
            homeLbl1.Content = ""; awayLbl1.Content = "";
            homeLbl2.Content = ""; awayLbl2.Content = "";
            homeLbl3.Content = ""; awayLbl3.Content = "";
            homeLbl4.Content = ""; awayLbl4.Content = "";
            homeLbl5.Content = ""; awayLbl5.Content = "";
            homeLbl6.Content = ""; awayLbl6.Content = "";
            homeLbl7.Content = ""; awayLbl7.Content = "";
            homeLbl8.Content = ""; awayLbl8.Content = "";
            homeLbl9.Content = ""; awayLbl9.Content = "";
            homeLbl10.Content = ""; awayLbl10.Content = "";

            instructionLbl.Content = "";
        }

        private void populateResultsClick(object sender, RoutedEventArgs e)
        {
            if (fixtures.doesFileExist())
            {
                Fixtures.FixturesBlock next_fixtures = fixtures.readFirstBlock();

                ResultsWkLbl.Content = next_fixtures.week_description;
                homeLbl1.Content = next_fixtures.fixtures[0].home;
                awayLbl1.Content = next_fixtures.fixtures[0].away;

                homeLbl2.Content = next_fixtures.fixtures[1].home;
                awayLbl2.Content = next_fixtures.fixtures[1].away;

                homeLbl3.Content = next_fixtures.fixtures[2].home;
                awayLbl3.Content = next_fixtures.fixtures[2].away;

                homeLbl4.Content = next_fixtures.fixtures[3].home;
                awayLbl4.Content = next_fixtures.fixtures[3].away;

                homeLbl5.Content = next_fixtures.fixtures[4].home;
                awayLbl5.Content = next_fixtures.fixtures[4].away;

                homeLbl6.Content = next_fixtures.fixtures[5].home;
                awayLbl6.Content = next_fixtures.fixtures[5].away;

                homeLbl7.Content = next_fixtures.fixtures[6].home;
                awayLbl7.Content = next_fixtures.fixtures[6].away;

                homeLbl8.Content = next_fixtures.fixtures[7].home;
                awayLbl8.Content = next_fixtures.fixtures[7].away;

                homeLbl9.Content = next_fixtures.fixtures[8].home;
                awayLbl9.Content = next_fixtures.fixtures[8].away;

                homeLbl10.Content = next_fixtures.fixtures[9].home;
                awayLbl10.Content = next_fixtures.fixtures[9].away;

                commitResBtn.IsEnabled = true;
                instructionLbl.Content = "Please set the scores correctly before committing";
            }
            else
            {
                var path = fixtures.getPathToFile();
                MessageBox.Show($"Can't find fixtures file: {path}. No fixtures found",
                                "Fixtures file not found",
                                MessageBoxButton.OK);

            }
        }

        private void home1LostFocus(object sender, RoutedEventArgs e)
        {
        }
    }
}
