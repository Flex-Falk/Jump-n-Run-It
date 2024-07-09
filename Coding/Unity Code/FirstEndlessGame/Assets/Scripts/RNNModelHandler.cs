using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;

// see collab skript
public enum UserAction
{
    Jump = 1,
    Shoot = 3,
    Crouch = 0,
    Neutral = 2,
};

public class RNNModelHandler : MonoBehaviour
{
    public NNModel modelAsset;
    private Model model;
    private IWorker worker;

    private string[] keys;
    private Dictionary<string, float> inputDict;

    // Start is called before the first frame update
    void Start()
    {
        model = ModelLoader.Load(modelAsset);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, model);

        keys = new string[] { "x", "y", "z", "yaw", "pitch", "roll" };
        inputDict = new Dictionary<string, float>(6);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // vielleicht nicht nötig
    private Dictionary<string, float> parseInputs(float[] inputs)
    {
        int index = 0;
        foreach (var key in keys)
        {
            inputDict[key] = inputs[index++];
        }
        return inputDict;
    }

    private int MaxInArray(float[] floats) {
        float c = float.MinValue;
        int indexMax = -1;
        for(int i=0; i< floats.Length; i +=1 )
        {
            if (floats[i] >= c)
            {
                c = floats[i];
                indexMax = i;
            }
        }
        return indexMax;
    }

    public UserAction GetUserAction(float[] data)
    {

        // Input tensor erstellen
        // Tensor inputTensor = new Tensor(1, 1,  data);
        Tensor inputTensor = new Tensor(1, data.Length, 1, 1, data);


        // Vorhersage treffen
        worker.Execute(inputTensor);
        Tensor outputTensor = worker.PeekOutput();

        // Ausgabe verarbeiten
        float[] predictions = outputTensor.ToReadOnlyArray();
        Debug.Log("Predictions: " + string.Join(", ", predictions));

        // Tensoren entsorgen
        inputTensor.Dispose();
        outputTensor.Dispose();

        int maxIndex = MaxInArray(predictions);
        if (maxIndex == -1) { return UserAction.Neutral; }
        else
        {
            return (UserAction) maxIndex;
        }
    }

    float[][] seq = new float[][]{
        new float[]{-5019.0f,23470.0f,7634.0f,-14.360418f,-19.668957f,-19.876141f},
        new float[]{-5591.0f,27582.0f,7211.0f,-13.529002f,-18.567438f,-19.57518f},
        new float[]{-8369.0f,26687.0f,7139.0f,-13.811018f,-18.471643f,-18.311039f},
        new float[]{-11699.0f,22100.0f,7059.0f,-15.035475f,-19.277781f,-14.932572f},
        new float[]{-15578.0f,17427.0f,5996.0f,-21.207737f,-20.4666f,-12.251103f},
        new float[]{-25273.0f,10737.0f,2789.0f,-29.427649f,-23.855116f,-8.140183f},
        new float[]{-24939.0f,4538.0f,4197.0f,-47.376305f,-26.376276f,-4.485165f},
        new float[]{-25005.0f,12752.0f,5266.0f,-62.027802f,-28.546284f,2.15706f},
        new float[]{-26196.0f,-15782.0f,6594.0f,-88.199348f,-28.281425f,6.477275f},
        new float[]{-27253.0f,-22324.0f,914.0f,-102.161095f,-23.648937f,16.214033f},
        new float[]{-27887.0f,-19877.0f,4589.0f,-113.087097f,-19.671341f,22.485985f},
        new float[]{-27773.0f,-1554.0f,10906.0f,-117.005463f,-17.334806f,22.236414f},
        new float[]{-26835.0f,-6510.0f,6585.0f,-121.647881f,-17.750044f,16.642509f},
        new float[]{-26077.0f,-10775.0f,3774.0f,-122.92939f,-20.976959f,7.598702f},
        new float[]{-25294.0f,-17870.0f,2209.0f,-122.441956f,-24.102129f,7.248987f},
        new float[]{-24993.0f,-23676.0f,-860.0f,-120.279739f,-27.141514f,10.803078f},
        new float[]{-24331.0f,-20957.0f,-1861.0f,-113.666977f,-28.32806f,13.204218f},
        new float[]{-23710.0f,-15666.0f,-3742.0f,-107.232986f,-30.993572f,12.760225f},
        new float[]{-22933.0f,-13027.0f,-4191.0f,-95.226784f,-33.565964f,9.099196f},
        new float[]{-22730.0f,-5367.0f,-2946.0f,-86.321915f,-36.888329f,1.072562f},
        new float[]{-22941.0f,2860.0f,-1892.0f,-72.129295f,-37.782143f,-4.732635f},
        new float[]{-23380.0f,11539.0f,962.0f,-62.240501f,-36.857086f,-13.824538f},
        new float[]{-20222.0f,19113.0f,3676.0f,-49.183418f,-34.959476f,-19.523731f},
        new float[]{-15408.0f,23036.0f,7302.0f,-42.288437f,-31.399384f,-25.70689f},
        new float[]{-13664.0f,24529.0f,9320.0f,-33.258495f,-29.620506f,-28.231247f},
        new float[]{-9466.0f,24071.0f,10478.0f,-27.782314f,-27.430836f,-29.805155f},
        new float[]{-7136.0f,23920.0f,10572.0f,-19.93651f,-26.271467f,-29.415785f}
    };

    [ContextMenu("TestRNNInput")]
    public void TestRNNInput()
    {
        foreach(var data in seq)
        {
            Debug.Log(GetUserAction(data));
        }
        
    }

    public void OnDestroy()
    {
        worker?.Dispose();
    }


}
