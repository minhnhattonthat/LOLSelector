using System;
using System.Collections.Generic;
using System.Linq;

namespace LOLSelector
{
    class MainClass
    {
        static List<Player> chosenPlayers = new List<Player>();
        static List<Player> players = new List<Player>();

        static LinkedList<Player> playerList = new LinkedList<Player>();

        static Dictionary<Role, List<Player>> dictionaryByRole = new Dictionary<Role, List<Player>>();
        static Dictionary<string, List<Player>> dictionaryByTeam = new Dictionary<string, List<Player>>();

        static Dictionary<string, List<Player>> dictionaryChosen = new Dictionary<string, List<Player>>();

        public static void Main(string[] args)
        {
            Player NHOCTY = new Player("NHOCTY", 57234, Role.Top, "YG");
            Player ARCHIE = new Player("ARCHIE", 2921, Role.Top, "GAM");
            Player HOPE = new Player("HOPE", 11649, Role.Top, "UTM");

            Player NAUL = new Player("NAUL", 75923, Role.Mid, "YG");
            Player OPTIMUS = new Player("OPTIMUS", 138348, Role.Mid, "GAM");
            Player WARZONE = new Player("WARZONE", 2489, Role.Mid, "CR");

            Player LEVI = new Player("LEVI", 171153, Role.Jungler, "GAM");
            Player VENUS = new Player("VENUS", 1359, Role.Jungler, "YG");
            Player TARZAN = new Player("TARZAN", 1356, Role.Jungler, "UTM");

            Player NOWAY = new Player("NOWAY", 120002, Role.Bot, "GAM");
            Player BIGKORO = new Player("BIGKORO", 82470, Role.Bot, "YG");
            Player CELEBRITY = new Player("CELEBRITY", 37871, Role.Bot, "RF");

            Player PALETTE = new Player("PALETTE", 58572, Role.Support, "YG");
            Player SYA = new Player("SYA", 3008, Role.Support, "GAM");
            Player COMBATLAO = new Player("COMBATLAO", 21057, Role.Support, "EHUB");

            playerList.AddLast(NHOCTY);
            playerList.AddLast(ARCHIE);
            playerList.AddLast(HOPE);
            playerList.AddLast(NAUL);
            playerList.AddLast(OPTIMUS);
            playerList.AddLast(WARZONE);
            playerList.AddLast(LEVI);
            playerList.AddLast(VENUS);
            playerList.AddLast(TARZAN);
            playerList.AddLast(NOWAY);
            playerList.AddLast(BIGKORO);
            playerList.AddLast(CELEBRITY);
            playerList.AddLast(PALETTE);
            playerList.AddLast(SYA);
            playerList.AddLast(COMBATLAO);

            players.Add(NHOCTY);
            players.Add(ARCHIE);
            players.Add(HOPE);
            players.Add(NAUL);
            players.Add(OPTIMUS);
            players.Add(WARZONE);
            players.Add(LEVI);
            players.Add(VENUS);
            players.Add(TARZAN);
            players.Add(NOWAY);
            players.Add(BIGKORO);
            players.Add(CELEBRITY);
            players.Add(PALETTE);
            players.Add(SYA);
            players.Add(COMBATLAO);

            NormalChoosing();
            Console.WriteLine("".PadLeft(20,'='));
            GreedyChoosing();

        }

        static void NormalChoosing()
        {
            Dictionary<string, int> count = new Dictionary<string, int>();

            List<Player> list = playerList.ToList();
            list.Sort();
            list.Reverse();

            foreach (Player player in list)
            {
                if (!count.ContainsKey(player.Team))
                {
                    count.Add(player.Team, 0);
                }
            }
            playerList = new LinkedList<Player>(list);

            List<Player> chosenList = new List<Player>();

            while (playerList.Count > 0)
            {
                Player chosenPlayer = playerList.First();

                if (count[chosenPlayer.Team] < 2)
                {
                    count[chosenPlayer.Team]++;
                    chosenList.Add(chosenPlayer);
                    playerList.RemoveFirst();
                    var removeList = playerList.Where(x => x.Role == chosenPlayer.Role).ToList();
                    foreach (Player player in removeList)
                    {
                        playerList.Remove(player);
                    }
                }
                else
                {
                    playerList.RemoveFirst();
                }
            }

            int vote = 0;
            foreach (Player player in chosenList)
            {
                vote += player.Vote;
                Console.WriteLine("{0}: {1} from {2}, {3}", player.Role, player.Name, player.Team, player.Vote);
            }
            Console.WriteLine("Total vote: {0}", vote);
        }

        static void GreedyChoosing()
        {
            //Create hashmap of players
            foreach (Player player in players)
            {
                if (!dictionaryByRole.ContainsKey(player.Role))
                {
                    List<Player> l = new List<Player>();
                    l.Add(player);
                    dictionaryByRole.Add(player.Role, l);
                }
                else
                {
                    dictionaryByRole[player.Role].Add(player);
                }
            }

            Dictionary<int, List<Player>> votesList = new Dictionary<int, List<Player>>();

            var keys = dictionaryByRole.Keys;

            List<Player> item = new List<Player>();


            List<Player> tops = dictionaryByRole[Role.Top];
            List<Player> bots = dictionaryByRole[Role.Bot];
            List<Player> mids = dictionaryByRole[Role.Mid];
            List<Player> junglers = dictionaryByRole[Role.Jungler];
            List<Player> supports = dictionaryByRole[Role.Support];


            for (int i = 0; i < tops.Count; i++)
            {
                item.Add(tops[i]);
                for (int j = 0; j < mids.Count; j++)
                {
                    item.Add(mids[j]);
                    for (int k = 0; k < bots.Count; k++)
                    {
                        item.Add(bots[k]);
                        for (int l = 0; l < junglers.Count; l++)
                        {
                            item.Add(junglers[l]);
                            for (int m = 0; m < supports.Count; m++)
                            {
                                item.Add(supports[m]);
                                int vote = SumVote(item);
                                votesList.Add(vote, item.ToList());
                                item.Remove(supports[m]);
                            }
                            item.Remove(junglers[l]);
                        }
                        item.Remove(bots[k]);
                    }
                    item.Remove(mids[j]);
                }
                item.Remove(tops[i]);
            }

            var votes = votesList.Keys;
            var sortedVotes = votes.ToList();
            sortedVotes.Sort();

            int maxVote = 0;
            for (int i = 0; i < votes.Count; i++)
            {
                List<Player> pl = votesList[sortedVotes[i]];
                Dictionary<string, int> count = new Dictionary<string, int>();
                bool isLegit = true;
                foreach(Player player in pl)
                {
                    if(!count.ContainsKey(player.Team))
                    {
                        count.Add(player.Team, 1);
                    }
                    else
                    {
                        count[player.Team]++;
                        isLegit &= count[player.Team] <= 2;
                    }
                }
                if(isLegit)
                {
                    maxVote = sortedVotes[i];
                }
            }

            foreach (Player player in votesList[maxVote])
            {
                Console.WriteLine("{0}: {1} from {2}, {3}", player.Role, player.Name, player.Team, player.Vote);
            }
            Console.WriteLine("Max vote: {0}", maxVote);
        }

        static int SumVote(List<Player> list)
        {
            int vote = 0;
            for (int i = 0; i < list.Count; i++)
            {
                vote += list[i].Vote;
            }
            return vote;
        }

    }

    class Player : IComparable
    {
        string name;
        int vote;
        Role role;
        string team;
        bool isChosen;

        public Player()
        {

        }

        public Player(string name, int vote, Role role, string team)
        {
            this.name = name;
            this.vote = vote;
            this.role = role;
            this.team = team;
        }

        public string Name { get => name; set => name = value; }
        public int Vote { get => vote; set => vote = value; }
        public Role Role { get => role; set => role = value; }
        public string Team { get => team; set => team = value; }
        public bool IsChosen { get => isChosen; set => isChosen = value; }

        public int CompareTo(object obj)
        {
            if (obj is Player)
            {
                if (this.Vote > ((Player)obj).Vote) return 1;
                else if (this.Vote < ((Player)obj).Vote) return -1;
                else return 0;
            }
            else
            {
                throw new FormatException();
            }
        }
    }

    enum Role
    {
        Top, Mid, Bot, Jungler, Support
    }
}
