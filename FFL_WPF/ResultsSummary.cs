using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

/// <summary>
/// Summary description for ResultsSummary
/// This class is used to give an analysis of the results to date
/// </summary>

namespace FFL_WPF
{
    using ResultsByTeam =
       System.Collections.Generic.Dictionary<CommonTypes.TeamName, 
                                             List<CommonTypes.TeamResult>>;

    using GoalsByTeam = Dictionary<CommonTypes.TeamName, CommonTypes.GoalsCount>;

    using RankingByTeam = Dictionary<CommonTypes.TeamName, ushort>;

    using FixturesByTeam = Dictionary<CommonTypes.TeamName, List<CommonTypes.Fixture>>;

    public class ResultsSummary
    {
        /// <summary>
        /// Details of next few fixtures for a particular team
        /// </summary>
        private class FixtureDetails
        {
            public ushort num_fixtures_stored;
            public string[] next_fixtures;
            public ushort num_goals; // Conceded or scored dependant on use
            public ushort num_home_matches; // Num home matches in next few fixtures
            public ushort ave_opp_ranking; // Next 6 matches
            public const ushort NUM_MATCHES_CONSIDERED = 6;

            public FixtureDetails(ushort num_fixtures = NUM_MATCHES_CONSIDERED)
            {
                num_fixtures_stored = num_fixtures;
                next_fixtures = new string[num_fixtures_stored];
            }
        }

        /// <summary>
        /// formFixtureFields will return info needed to populate the upcoming fixture columns of
        /// the Attacking or Defending dataGridView. 
        /// It should be called with goals conceded for the goals_by_team parameter and 
        /// the defensive ranking list for the team_ranking to provide fixture data for the 
        /// Attacking dataGridView, and vice-versa for the Defending view.
        /// </summary>
        /// <param name="goals_by_team">Goals scored/conceded as appropriate</param>
        /// <param name="fixtures_by_team"></param>
        /// <param name="team_ranking">Attacking/defensive ranking</param>
        /// <param name="team_nm"></param>
        /// <returns></returns>
        private static FixtureDetails formFixtureFields(
                                    GoalsByTeam goals_by_team,
                                    FixturesByTeam fixtures_by_team,
                                    RankingByTeam team_ranking,
                                    CommonTypes.TeamName team_nm)
        {
            var result = new FixtureDetails();

            List<CommonTypes.Fixture> next_fixtures = fixtures_by_team[team_nm];

            ushort num_home_matches = 0;
            ushort tot_opp_goals = 0; // Scored/conceded as appropriate for this call
            ushort tot_opp_ranking = 0;

            for (int idx = 0; idx < FixtureDetails.NUM_MATCHES_CONSIDERED; ++idx)
            {
                // If we haven't got enough fixtures then just use what we have
                if ((next_fixtures.Count - idx - 1) < 0)
                {
                    result.next_fixtures[idx] = "-";
                    break;
                }

                CommonTypes.Fixture curr_fixture = next_fixtures[idx];
                CommonTypes.TeamName opponent = curr_fixture.opponent;

                tot_opp_goals += goals_by_team[opponent].last6;

                string opponent_str = GenUtils.ToShortString(opponent);

                ushort ranking = team_ranking[opponent];
                opponent_str += ranking;

                tot_opp_ranking += ranking;

                if (curr_fixture.is_home)
                {
                    opponent_str += 'h';
                    ++num_home_matches;
                }
                else
                {
                    opponent_str += 'a';
                }

                result.next_fixtures[idx] = opponent_str;
            } // end opponent loop
            result.num_goals = tot_opp_goals;
            result.num_home_matches = num_home_matches;

            result.ave_opp_ranking = (ushort)(tot_opp_ranking / result.num_fixtures_stored);

            return result;
        }

        DataGrid attacking_grid;
        DataGrid defending_grid;

        public ResultsSummary(DataGrid att_grid, DataGrid def_grid)
        {
            attacking_grid = att_grid;
            defending_grid = def_grid;
        }

        private void resetGrid(DataGrid dataGridView)
        {
#warning "TO DO"
        }

#warning "Consider moving this definition"
        private struct AttackingSummaryRow
        {
            public string Att_Rank { get; set; }
            public string TeamName { get; set; }
            public int GoalsScoredLast10Matches { get; set; }
            public int GoalsScoredLast6 { get; set; }
            public int GoalsScoredOverall { get; set; }

            public string NextFixture1 { get; set; }
            public string NextFixture2 { get; set; }
            public string NextFixture3 { get; set; }
            public string NextFixture4 { get; set; }
            public string NextFixture5 { get; set; }
            public string NextFixture6 { get; set; }
            public int NumHomeMatchesNext6 { get; set; }

            public int AveOppRankNext6 { get; set; }
        }

        /// <summary>
        /// Will add rows to the attacking dataGridView, with individual columns set up appropriately.
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="fixtures_by_team"></param>
        /// <param name="goals_scored_by_team"></param>
        /// <param name="goals_conceded_by_team"></param>
        /// <param name="attacking_ranking"></param>
        /// <param name="defending_ranking"></param>
        private void populateAttackingTab(
                          DataGrid       dataGridView,
                          FixturesByTeam fixtures_by_team,
                          GoalsByTeam    goals_scored_by_team,
                          GoalsByTeam    goals_conceded_by_team,
                          RankingByTeam  attacking_ranking,
                          RankingByTeam  defending_ranking)
        {
            resetGrid(dataGridView);

            var row_list = new List<AttackingSummaryRow>();

            foreach (CommonTypes.TeamName team_nm in Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                var curr_row = new AttackingSummaryRow();

                List<CommonTypes.Fixture> next_fixtures = fixtures_by_team[team_nm];

                string[] curr_row_str = new string[13];

                ushort att_rank = attacking_ranking[team_nm];

                if(att_rank < 10)
                  curr_row.Att_Rank = "0" + att_rank.ToString();
                else
                  curr_row.Att_Rank = att_rank.ToString();

                curr_row.TeamName = GenUtils.ToShortString(team_nm);
                curr_row.GoalsScoredLast10Matches = goals_scored_by_team[team_nm].last10;
                curr_row.GoalsScoredLast6 = goals_scored_by_team[team_nm].last6;
                curr_row.GoalsScoredOverall = goals_scored_by_team[team_nm].total;

                FixtureDetails fixture_dets = formFixtureFields(goals_conceded_by_team,
                                                                fixtures_by_team,
                                                                defending_ranking,
                                                                team_nm);
                curr_row.NextFixture1 = fixture_dets.next_fixtures[0];
                curr_row.NextFixture2 = fixture_dets.next_fixtures[1];
                curr_row.NextFixture3 = fixture_dets.next_fixtures[2];
                curr_row.NextFixture4 = fixture_dets.next_fixtures[3];
                curr_row.NextFixture5 = fixture_dets.next_fixtures[4];
                curr_row.NextFixture6 = fixture_dets.next_fixtures[5];

                curr_row.NumHomeMatchesNext6 = fixture_dets.num_home_matches;
                curr_row.AveOppRankNext6 = fixture_dets.ave_opp_ranking;

                row_list.Add(curr_row);
            }

            dataGridView.ItemsSource = row_list;
        }

        private struct DefendingSummaryRow
        {
            public string Def_Rank { get; set; }
            public string Team_Name { get; set; }
            public int GoalsAgainstLast10Matches { get; set; }
            public int GoalsAgLast6 { get; set; }
            public int GoalsAgOverall { get; set; }
            public int CleanSheetsLast10Matches { get; set; }
            public int CleanSheetsLast6 { get; set; }
            public int CleanSheetsOverall { get; set; }
            public string NextFixtureWithOpponentsAttRank1 { get; set; }
            public string Next_Fixture2 { get; set; }
            public string Next_Fixture3 { get; set; }
            public string Next_Fixture4 { get; set; }
            public string Next_Fixture5 { get; set; }
            public string Next_Fixture6 { get; set; }

            public int Num_Home_Matches_In_Next_6 { get; set; }

            public int Ave_Opponent_Rank_Next_6 { get; set; }
        }
        /// <summary>
        /// Will add rows to the defending dataGridView, with individual columns set up appropriately.
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="fixtures_by_team"></param>
        /// <param name="goals_scored_by_team"></param>
        /// <param name="goals_conceded_by_team"></param>
        /// <param name="clean_sheets_by_team"></param>
        /// <param name="attacking_ranking"></param>
        /// <param name="defending_ranking"></param>

        private void populateDefendingTab(
                  DataGrid       dataGridView,
                  FixturesByTeam fixtures_by_team,
                  GoalsByTeam    goals_scored_by_team,
                  GoalsByTeam    goals_conceded_by_team,
                  GoalsByTeam    clean_sheets_by_team,
                  RankingByTeam  attacking_ranking,
                  RankingByTeam  defending_ranking)
        {
            // Define column indices

            resetGrid(dataGridView);

            var row_list = new List<DefendingSummaryRow>();

            foreach (CommonTypes.TeamName team_nm in Enum.GetValues(typeof(CommonTypes.TeamName)))
            {
                var curr_row = new DefendingSummaryRow();

                List<CommonTypes.Fixture> next_fixtures = fixtures_by_team[team_nm];

                ushort att_rank = attacking_ranking[team_nm];

                if(att_rank < 10)
                    curr_row.Def_Rank = "0" + att_rank.ToString();
                else
                    curr_row.Def_Rank = att_rank.ToString();

                curr_row.Team_Name = GenUtils.ToShortString(team_nm);
                curr_row.GoalsAgainstLast10Matches = goals_conceded_by_team[team_nm].last10;
                curr_row.GoalsAgLast6 = goals_conceded_by_team[team_nm].last6;
                curr_row.GoalsAgOverall = goals_conceded_by_team[team_nm].total;

                curr_row.CleanSheetsLast10Matches = clean_sheets_by_team[team_nm].last10;
                curr_row.CleanSheetsLast6 = clean_sheets_by_team[team_nm].last6;
                curr_row.CleanSheetsOverall = clean_sheets_by_team[team_nm].total;

                FixtureDetails fixture_dets = formFixtureFields(goals_conceded_by_team,
                                                                fixtures_by_team,
                                                                defending_ranking,
                                                                team_nm);
                curr_row.NextFixtureWithOpponentsAttRank1 = fixture_dets.next_fixtures[0];
                curr_row.Next_Fixture2 = fixture_dets.next_fixtures[1];
                curr_row.Next_Fixture3 = fixture_dets.next_fixtures[2];
                curr_row.Next_Fixture4 = fixture_dets.next_fixtures[3];
                curr_row.Next_Fixture5 = fixture_dets.next_fixtures[4];
                curr_row.Next_Fixture6 = fixture_dets.next_fixtures[5];

                curr_row.Num_Home_Matches_In_Next_6 = fixture_dets.num_home_matches;
                curr_row.Ave_Opponent_Rank_Next_6 = fixture_dets.ave_opp_ranking;

                row_list.Add(curr_row);
            }

            dataGridView.ItemsSource = row_list;
        }

        public void reCalculate(List<CommonTypes.Result>   results,
                                List<CommonTypes.TwoTeams> fixtures)
        {
            // Calculate all results team by team
            ResultsByTeam results_by_team = ResultsCalculations.calcResultsByTeam(results);

            // Calculate goals per period team by team
            // 
            // Pass method callbacks in
            //
            var goals_scored_by_team =
                ResultsCalculations.calcGoalsScoredConcededByTeam(results_by_team, getGoalsScoredCB);

            var goals_conceded_by_team =
                ResultsCalculations.calcGoalsScoredConcededByTeam(results_by_team, getGoalsConcededCB);

            var clean_sheets_by_team =
                ResultsCalculations.calcGoalsScoredConcededByTeam(results_by_team, getCleanSheetsCB);

            var attacking_ranking = ResultsCalculations.calcAttackingDefRanking(goals_scored_by_team);
            var defending_ranking = ResultsCalculations.calcAttackingDefRanking(clean_sheets_by_team);

            var fixtures_by_team = FixturesCalculations.calcResultsByTeam(fixtures);
            populateAttackingTab(attacking_grid, 
                                 fixtures_by_team,
                                 goals_scored_by_team  : goals_scored_by_team,
                                 goals_conceded_by_team: goals_conceded_by_team,
                                 attacking_ranking     : attacking_ranking,
                                 defending_ranking     : defending_ranking);

            populateDefendingTab(defending_grid,
                                 fixtures_by_team,
                                 goals_scored_by_team  : goals_scored_by_team,
                                 goals_conceded_by_team: goals_conceded_by_team,
                                 clean_sheets_by_team  : clean_sheets_by_team,
                                 attacking_ranking     : attacking_ranking,
                                 defending_ranking     : defending_ranking);
        }

        //
        // Define the delegates
        //
        private static ushort getGoalsScoredCB(CommonTypes.TeamResult score) => score.goals_for;
        private static ushort getGoalsConcededCB(CommonTypes.TeamResult score) => score.goals_against;

        private static ushort getCleanSheetsCB(CommonTypes.TeamResult score)
        {
            ushort result = 0;

            if (score.goals_against == 0)
                result = 1;

            return result;
        }

     }
}