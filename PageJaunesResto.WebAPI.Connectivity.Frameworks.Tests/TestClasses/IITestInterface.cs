namespace PageJaunesResto.WebAPI.Connectivity.Frameworks.Tests.TestClasses
{
    interface IITestInterface
    {
        TestObjectShape GetItems();
        TestObjectShape GetItemsWithLoadsOfParams(string a, int b);
        TestObjectShape SetItems();
    }
}