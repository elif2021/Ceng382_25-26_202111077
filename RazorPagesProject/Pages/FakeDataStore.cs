public static class FakeDataStore
{
    public static List<ClassInformationTable> Data = new List<ClassInformationTable>
    {
        new ClassInformationTable { Id = 1, ClassName = "Math", StudentCount = 30, Description = "Mathematics Class" },
        new ClassInformationTable { Id = 2, ClassName = "English", StudentCount = 25, Description = "English Class" },
        // Diğer veriler...
    };
}