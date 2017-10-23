# LOLSelector

Based on list of LOL players from [this source](https://sukien.lienminh.garena.vn/), the program selects 5 most suitable players to form a team.
The program included 2 methods of choosing:
1. Normal choosing: Choose the most vote of each role that fulfilled hte condition of no more than 2 people being in a same team.
Flow chart:

![](https://github.com/minhnhattonthat/LOLSelector/blob/master/flowchart.png)

2. Greedy choosing: The same as above but also have to have the biggest total vote of 5 players.
