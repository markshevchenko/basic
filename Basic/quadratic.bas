10 PRINT "Input A, B, C: "
20 INPUT A
30 INPUT B
40 INPUT C
50 LET D = B * B - 4 * A * C
60 IF D < 0 THEN GOTO 120
70 LET X1 = (-B + SQRT(D)) / (2 * A)
80 LET X2 = (-B - SQRT(D)) / (2 * A)
90 PRINT "X1 = ", X1
100 PRINT "X2 = ", X2
110 END
120 PRINT "No solutions"
