# INFR 4320U - Artificial Intelligence in Games

## Winter 2022 - Assignment 1

### Daniel Presas - 100699431

1. You are in an adventure type game going through a set of rooms where in the first room you need to choose doors to go through to reach a treasure. The only information you can determine about a door before you open it is if the door is hot to the touch and if there is noise coming from the other side of the door. Luckily you are also given the following table of previously collected information on doors that have been encountered by previous game players as they played the game describing how many doors they encountered with each of the "Hot" and "Noisy" properties and if the door turned out to be a "Safe door" to go through. ("Y" indicates "yes" and "N" indicates "no"). So for instance, reading the first row of the table, sixteen hot, noisy, safe doors were encountered by previous players.

    **Note: Round all answers to three significant digits.**

    |  Hot  | Noisy | Safe door | Number of doors |
    | :---: | :---: | :-------: | :-------------: |
    |   Y   |   Y   |     Y     |       16        |
    |   Y   |   Y   |     N     |       12        |
    |   Y   |   N   |     Y     |        5        |
    |   Y   |   N   |     N     |        8        |
    |   N   |   Y   |     Y     |       13        |
    |   N   |   Y   |     N     |       10        |
    |   N   |   N   |     Y     |        7        |
    |   N   |   N   |     N     |        9        |
    | ----- | ----- | --------- | --------------- |
    |       |       | **Total** |       80        |

    Using the above table information:

    1. **(6 marks)** Compute the following probabilities:

        1. `P(Hot = N, Noisy = N, Safe door = Y)`

            $P(Hot = N, Noisy = N, Safe\ door = Y)$

            $ = \frac {7}{80}$

            $= 0.0875 = 8.75\%$

        2. `P(Hot = N, Safe door = N)`

            _let A = (Hot = N), let B(Safe door = N)_

            $P(Hot = N, Safe\ door = N)$

            $= P(A \cap B)$

            $= P(A) * P(B)$

            $= (\frac {13 + 10 + 7 + 9}{80}) * (\frac {12 + 8 + 10 + 9}{80})$

            $= 0.4875 * 0.4875$

            $= 0.23765625 = 23.8\%$

        3. `P(Hot = Y | Noisy = N)`

            _let A = (Hot = Y), let B(Noisy = N)_

            $P(Hot = Y | Noisy = N)$

            $= P(A|B)$

            $= \frac{P(A \cap B)} {P(B)}$

            $= \frac{P(A) * P(B)} {P(B)}$

            $= (\frac {16 + 12 + 5 + 8}{80} * \frac{5 + 8 + 7 + 9}{80}) \div \frac{5 + 8 + 7 + 9}{80}$

            $= \frac {0.5125 * 0.3625}{0.3625}$

            $= 0.5125 = 51.3\%$ _(Probability independent)_

    2. **(3 marks)** Consider the property of being "Hot" and the property of being "Noisy", are these two properties independent of each other? Support your answer with an explanation.

        _The probability of a door being hot and the probability of a door being noisy are properties that are independent of each other, because after testing that $P(Hot | Noisy) = P(Hot)$ (i.e. the probability of the door being hot does not change with the probability of the door being noisy), it is safe to conclude that they are independent._

    3. **(2 marks)** Compute the probability that a door is both safe and noisy.

        _let A = (Safe door = Y), let B(Noisy = Y)_

        $P(Safe\ door, Noisy)$

        $= P(A \cap B)$

        $= P(A) * P(B)$

        $= \frac {16 + 5 + 13 + 7}{80} * \frac{16 + 12 + 13 + 10}{80}$

        $= 0.5125 * 0.6375$

        $= 0.32671875 = 32.7\%$

    4. **(2 marks)** Compute the probability that a safe door is hot. **(Hint: you are only considering safe doors.)**

        _let A = (Safe door = Y), let B = (Hot = Y)_

        $P(Safe\ door, Hot)$

        $P(A \cap B)$

        $= P(A) * P(B)$

        $= \frac {16 + 5 + 13 + 7}{80} * \frac{16 + 12 + 5 + 8}{80}$

        $= 0.5125 * 0.5125$

        $= 0.26265625 = 26.3\%$

    5. **(3 marks)** You are now playing the game. You encounter a door that is noisy but not hot, should you open the door or not? Support your answer with probabilities computed from the table.

        $P(Hot, Noisy, Safe\ door = Y) = 13/80 = 16.3\%$
        $P(Hot, Noisy, Safe\ door = N) = 10/80 = 12.5\%$

        _Since the probability that the door is safe is greater, the player should open the door noisy but not hot door._

***

2. **(19 marks)** Implement this first room scenario in this adventure game using any game engine you wish. A minimum of 20 doors must be available to choose from in this room. Your program must accept a text file of probabilities. A sample text file is provided in the assignment account posting called `probabilities.txt`. Your program must behave using the probabilities of the inputted probability file. Your program will be tested using a different probability file that is not provided to you. Your program should prompt the user at the start for the name and location of the input probability file to use (you cannot assume the file is always called `probabilities.txt`). Your program must implement some graphic/sound to provide the player with feedback that a door is hot and/or noise is coming from it. You will be marked on creativity of your game and correctness of the behaviour. For example, a 2D game with only geometric shapes for doors with a top-down view of the room would generally be marked lower on creativity/complexity than one including a graphic design for the doors.

    The TA will mark the execution of your program using a test probability file during the lab times. Be sure to attend the lab so that the TA may get feedback with any issues running/testing your program. Be sure to document your code including your name, date, variables and method/functions.

    _Submission:_ `https://github.com/DanielPresas/AI_Assignment1`
