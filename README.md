# WebAPI.Connectivity

API for connecting with any WebAPI (or other web resource, given convention tweaking)

This framework is written with specifically connecting to a WebAPI resource in mind, as usually you will have defined some sort of C# Interface for your WebAPI, and defined some conventions for how the routing works. Using this knowledge, the API tries to create the correct shape of requests given an expression using the interface relating to the web request in question.

This is all perhaps easier explained in code - so, take a peek at the examples following for an idea of usage.

Given a sample piece of code as below

```csharp
    interface IITestInterface
    {
        TestObjectShape GetItems();
        TestObjectShape GetItemsWithLoadsOfParams(string a, int b);
        TestObjectShape SetItems();
    }
    
    class TestController : ITestInterface, ApiController
    {
       // imagine some implementations
    }
    
    class TestObjectShape
    {
        public int Number { get; set; }
        public string String { get; set; }
    }
```

You simply pass in an expression to the RequestGenerator, with the baseUrl for your API service, and an expression containing the method and parameters that you'd like to execute, or make a request to be deserialized. (note, all service calls are async, and as such require to be awaited.)

```csharp
  TestObjectShape result = await new RequestGenerator("http://www.chronoresto.fr/API/?key=12345")
                .InterfaceAndMethodToRequest<IITestInterface>(x => x.GetItemsWithLoadsOfParams("hello", 123));
```
In this scenario, this will create task for a GET request, to the effect of "http://www.chronoresto.fr/API/Test/GetItemsWithLoadsOfParams/?key=12345&a=hello&b=123", and deserialize it into a `TestObject` - all ready to be `awaited`.

As it happens for us, we keep the Get prefix, but all this sort of stuff can be easily switched out for whatever strategy works for you - documentation for how on this to come!
