![.NET Core](https://github.com/aimenux/ChannelsDemo/workflows/.NET%20Core/badge.svg)
# ChannelsDemo
```
Using System.Threading.Channels
```

> Multiple examples are implemented :
>
> **(1)** Example1 : based on hardcoded implementation of channel concept
>> Weak consumer using `Read` method -> may consume inexistants items (0 value)
>
> **(2)** Example2 : based on hardcoded implementation of channel concept
>> Enhanced consumer using `TryRead` method -> consume always existants items
>
> **(3)** Example3 : based on channel built-in implementation (unbounded)
>> Consumer using `WaitToReadAsync` method
>
> **(4)** Example4 : based on channel built-in implementation (unbounded)
>> Consumer using `ReadAllAsync` method
>
> **(5)** Example5 : based on channel built-in implementation (bounded)
>> Consumer using `WaitToReadAsync` method
>
> **(6)** Example6 : based on channel built-in implementation (bounded)
>> Consumer using `ReadAllAsync` method

**`Tools`** : vs19, net core 3.1