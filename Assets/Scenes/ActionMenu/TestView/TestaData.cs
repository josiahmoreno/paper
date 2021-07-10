using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestData : ITestData
{
    public string Message { get; set; }

    public TestData(string message)
    {
        Message = message;
    }
}
