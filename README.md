# theory_of_language_project_2
# input
You will be given a Finite Automata.
# output
A positive integer representing the number of equivalent DFA states.
# example
input:
<br>
{q0,q1,q2,q3,q4}
<br>
{a,b}
<br>
{q1,q3}
<br>
6
<br>
q0,a,q1
<br>
q1,b,q2
<br>
q1,$,q3
<br>
q3,b,q4
<br>
q2,a,q3
<br>
q4,a,q2
<br>
output:
<br>
8
