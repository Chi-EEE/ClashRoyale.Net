import math

v = 300
g = 60

X1 = -9000
Y1 = 0

X2 = 2500
Y2 = 0

X0 = X2 - X1
Y0 = Y2 - Y1

top = math.pow(v, 2) + math.sqrt(math.pow(v, 4) - (g * math.pow(X0, 2) + (2 * Y0 * math.pow(v, 2))))
bottom = g * X0

angle = math.atan(top / bottom)

print(angle)