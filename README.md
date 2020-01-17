# BlinkFatigue
A SCP:SL Plugin that makes SCP-173 both interesting and scary to play against and fun to play as.

Requires [EXILED](https://github.com/galaxy119/EXILED/).

# Latest release
[Get the latest release here!](https://github.com/RogerFK/BlinkFatigue/releases/latest)

## Features

- **An incremental (and fully modifiable!) blink speed**, so the more you (or anyone) looks at him, the more you will blink and the faster he will come to snap your neck.
- **Avoids tutorials from raising the blink speed.** Whether your fellow tutorials are moderators just moving around and happen to spot SCP-173, or whether they are Serpent's Hand (plugin not currently available in any framework) trying to stack the blink speed for SCP-173 to cheat the system and make SCP-173 a really spooky boi, you're covered. I know, sounds crazy, but you can't actually edit this.

## Configs

:warning: **Making these configs "too OP" may result in your players getting epileptic seizures. __Change at YOUR own risk.__**

| Config Option | Value Type | Default Value | Description |
|:------------------------:|:----------:|:-------------:|:------------------------------------------:|
| `blink_enable` | bool | `true` | Enables/disables the plugin |
| `blink_minblinktime` :warning: | number | 1.5 | **Making people blink too fast may cause epileptic seizures. Change this config at your own risk.** Minimum time allowed between blinks. |
| `blink_mintime` | number | 2.5 | The minimum time between blinks. Explained further below. |
| `blink_maxtime` | number | 3.5 | The maximum time between blinks. Explained further below. |
| `blink_addmin` | number | 0.35 | The minimum time to add to the rework value. Explained further below. |
| `blink_addmax` | number | 0.45 | The maximum time to add to the rework value. Explained further below. |
| `blink_decreaserate` | number | 0.75 | The rate at which the cooldown of the blink speed is applied. Explained further below. |

## Explanations and things to consider

### Minimum time
I think there's no need to explain what the minimum time between blinks. Also, making it 0.2 may be funny to you and a few friends, but may cause epileptic seizures to other people. Either contact a medic specialized on the topic, or don't take the risk for a few gigs. **You have been warned.**

### Mintime, maxtime
The minimum and maximum blink times are the default values the game decides for how much time the players will have their eyes open, randomly. In other words, if it's between 2.5 seconds and 3.5 seconds, after every blink is done the game picks a new random number, let's say 2.7. After a blink, it picks another random number, let's say 3.2. If you want to go for a 100% competitive setup, this is a good plugin for you since you can tweak it to be a fixed number by setting both numbers as the same one (untested, I would do 2.99 as minimum and 3 as maximum just in case).

### Decrease rate
In a nutshell, if you set it to 1 and the plugin was substracting 3 seconds from the regular blink time, it would take 3 seconds for the blink to get to the initial state (which is the default, the one that only picks a number between 2.5 and 3.5

### Addmin, addmax.
### These are pretty confusing, so you should stop reading here unless you're really scientifically curious about it.
The "addmin" and "addmax" values are, like mintime and maxtime, random values which add to an internal number stored by the plugin that keeps incrementing the more people blink and gets decremented when people stop looking at him. __In a nutshell, this is what makes people blink faster everytime they blink__.

Basically, every time a player blinks, the next blink will take less time, and this time is a random number between addmin and addmax. So, the time between blinks would be a random number between 2.5 and 3.5, let's say 2.6 seconds, and that number gets decremented with a number between 0.35 and 0.45, let's say 0.31, so it'd be 2.6 - 0.31 = 2.29 seconds

Since this is so random, here goes a few examples taking addmin and addmax values of 0.20 and 0.40 respectively:

- __Bad luck for SCP-173:__

First blink takes 2.8 - 0, second blink takes 3.1 - (0 + 0.24) = 2.86, third blink takes 3.2 - (0 + 0.24 + 0.23) = 2.73

Seems like the plugin doesn't work, but it does. Basically, the first number is the first random number generated between mintime and maxtime, and 0 is the initial value of the internal variable mentioned earlier, 0.24 is the next random value, and 0.23 is the next one. Don't worry, it all gets added internally.

- **Good luck for SCP-173:**

First blink takes 3.4 - 0, second blink takes 2.8 - 0.33 = 2.47, third blink takes 2.6 - 0.66 = 1.84

Wowzer, that escalated *quickly*. Again, the first number is the random number between mintime and maxtime, and the second one is the internal value that will be substracted from the original blink time.
