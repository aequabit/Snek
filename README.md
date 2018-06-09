# Snek

### Implementation of Snake for a school project

# Requirements

* [x] Random positioning at start
* [x] Using [Listard](https://github.com/aequabit/Listard) for storing entities
* [ ] No usage of .NET classes other than _System.Console_, _System.Timers.Timer_ and _System.Random_

# To do:
* [ ] ? Make the snake independent from the entity list
* [ ] Different kind of food that decreases the snake's length
* [x] Fully drawing the snake at start instead of building it up slowly
* [ ] Fixing the snake colliding with itself when changing directions quickly (allow only one direction change per tick)
* [ ] Shutdown of async workers and threads
* [ ] Resizing and re-rendering on runtime
* [x] Proper UI rendering (status bar is currently an entity)
* [x] Clean up some messy parts of the code
