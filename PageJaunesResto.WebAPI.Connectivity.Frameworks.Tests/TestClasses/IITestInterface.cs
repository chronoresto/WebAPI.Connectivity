namespace PageJaunesResto.WebAPI.Connectivity.Frameworks.Tests
{
    interface IITestInterface
    {
        TestObjectShape GetItems();
        TestObjectShape GetItemsWithLoadsOfParams(string a, int b);
        TestObjectShape SetItems();
    }
}