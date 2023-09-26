# Predatory Simulation of Animals

## Description
This program simulates the predatory relationship of cats and snake with birds. Here, the birds are the prey.

- The animals move about on the window.
- Although visually it seems like a 2D space, in actuality, there are moving in a 3D space.
- Every animal has a speed with which they move.
- Every animal has a range in which the other animals may fall into.
- Birds and Snake have a smell and hear property with which they can store the animals they have smelt.
- Cats and Snakes move to the nearest bird in the plane.
- If the bird is within the range of the snake or the cat, the bird is eaten and it dissappears from the screen.
- This simulation keeps going until all the birds have been eaten.

## Movement and Ranges
### Cats (C)
- Range 8
- Speed 16

### Snakes (S)
- Range 3
- Speed 14

### Birds (B)
- Move in a random (-10, 10) along the X axis.
- Move in a random (-10, 10) along the Y axis.
- Move in a random (-2, 2) along the Z axis.
