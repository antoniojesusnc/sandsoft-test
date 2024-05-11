using System;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    [Header("Test01")]
    [SerializeField]
    private List<TestSolutions<string, int>> _inputsTest01 = new ();
    [Header("Test02")]
    [SerializeField]
    private List<TestSolutions<string, string>> _inputsTest02 = new ();
    
    private Test01 _test01 = new Test01();
    
    private Test02 _test02 = new Test02();
    private Test02Alternative _test02Alternative = new Test02Alternative();
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckTest01(_inputsTest01);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CheckTest02(_inputsTest02);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CheckTest02Alternative(_inputsTest02);
        }
    }
    
    private void CheckTest01(List<TestSolutions<string, int>> input)
    {
        var error = false;
        for (int i = 0; i < input.Count; i++)
        {
            var solution = _test01.Solution(input[i].Input);
            if (solution != input[i].Solution)
            {
                Debug.Log($"Error in Test 01, it is \"{solution}\" but should be \"{input[i].Solution}\"");
                error = true;
            }
        }

        if (!error)
        {
            Debug.Log("[GOOD] Test Success");
        }
    }
    
    private void CheckTest02(List<TestSolutions<string, string>> input)
    {
        var error = false;
        for (int i = 0; i < input.Count; i++)
        {
            var solution = _test02.Solution(input[i].Input);
            if (solution != input[i].Solution)
            {
                Debug.Log($"Error in Test 02, it is \"{solution}\" but should be \"{input[i].Solution}\"");
                error = true;
            }
        }

        if (!error)
        {
            Debug.Log("[GOOD] Test Success");
        }
    }
    
    private void CheckTest02Alternative(List<TestSolutions<string, string>> input)
    {
        var error = false;
        for (int i = 0; i < input.Count; i++)
        {
            var solution = _test02Alternative.Solution(input[i].Input);
            if (solution != input[i].Solution)
            {
                Debug.Log($"Error in Test 02, it is \"{solution}\" but should be \"{input[i].Solution}\"");
                error = true;
            }
        }

        if (!error)
        {
            Debug.Log("[GOOD] Test Success");
        }
    }
    
    [Serializable]
    public class TestSolutions<T1, T2>
    {
        [field: SerializeField]
        public T1 Input { get; private set; }
        [field: SerializeField]
        public T2 Solution { get; private set; }
    }
}
