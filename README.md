# WebAPI.Connectivity

API for connecting with any WebAPI (or other web resource, given convention tweaking)

This framework is written with specifically connecting to a WebAPI resource in mind, as usually you will have defined some sort of C# Interface for your WebAPI, and defined some conventions for how the routing works. Using this knowledge, the API tries to create the correct shape of requests given an expression using the interface relating to the web request in question.

This is all perhaps easier explained in code - so, take a peek at the examples following for an idea of usage.

Given a sample piece of code as below

```csharp
    interface IITestInterface
    {
        TestObjectShape Get();
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
  TestObjectShape result = await new RequestGenerator("http://www.chronoresto.fr/API/")
                .InterfaceAndMethodToRequest<IITestInterface, IEnumerable<TestObjectShape>>(x => x.GetItemsWithLoadsOfParams("hello", 123));
```
In this scenario, this will create task for a GET request, to the effect of "http://www.chronoresto.fr/API/TestInterface/?key=12345&a=hello&b=123", and deserialize it into a collection of `TestObject` - all ready to be `awaited`.


```csharp
  TestObjectShape result = await new RequestGenerator("http://www.chronoresto.fr/API/")
                .InterfaceAndMethodToRequest<IITestInterface, TestObjectShape>(x => x.Get());
```

In this scenario, this will create task for a GET request, to the effect of "http://www.chronoresto.fr/API/TestInterface/", and deserialize it into a `TestObject` - all ready to be `awaited`.


With the strategy here, we keep the requests in the standard REST-style API conventions. Other strategies to apply this API to a wider variety of scenarios are coming soon.
