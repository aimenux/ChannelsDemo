# ChannelsDemo
```
Using System.Threading.Channels
```

> Multiple examples are implemented :

>> **(1)** Example1 : based on hardcoded implementation of channel concept
>> - weak consumer using `Read` method -> may return inexistants items (0 value)
>>
>> **(2)** Example2 : based on hardcoded implementation of channel concept
>> - enhanced consumer using `TryRead` method -> return always existants items
>>
>> **(3)** Example3 : based on channel built-in implementation (unbounded)
>> - consumer using `WaitToReadAsync` method
>>
>> **(4)** Example4 : based on channel built-in implementation (unbounded)
>> - consumer using `ReadAllAsync` method
>>
>> **(5)** Example5 : based on channel built-in implementation (bounded)
>> - consumer using `ReadAllAsync` method
>>

**`Tools`** : vs19, net core 3.1