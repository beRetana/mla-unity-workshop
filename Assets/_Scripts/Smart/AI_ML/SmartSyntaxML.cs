using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SmartSyntaxML : Agent
{
    [SerializeField] private string _wallTag = "Wall";
    [SerializeField] private string _targetTag = "Target";
    [SerializeField] private GameObject _target;
    [SerializeField] private TrainningVisuals _trainingVisuals;

    private float _maxSteps;
    private float _commulativeReward;
    private float _reward;
    private float _episodeCount;

    private SmartSyntaxController _smartSyntaxController;

    void Start()
    {
        _smartSyntaxController = GetComponent<SmartSyntaxController>();
    }

    /// <summary>
    /// Gets called right before Start()
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();

        _maxSteps = MaxStep;

    }

    /// <summary>
    /// Reset the agent and the player dummy position to a random one.
    /// </summary>
    public override void OnEpisodeBegin()
    {
        _smartSyntaxController.ResetSyntax();
        ResetRaffleTicket();
        _commulativeReward = 0;
        _episodeCount += 1;
    }

    /// <summary>
    /// Reset the position of our target to something random.
    /// </summary>
    private void ResetRaffleTicket()
    {
        float xDelimeter = 6.5f;
        float zDelimeter = 6.5f;
        float randomX = Random.Range(-xDelimeter, xDelimeter);
        float randomZ = Random.Range(-zDelimeter, zDelimeter);
        _target.transform.localPosition = new Vector3(randomX, 1f, randomZ);
    }

    /// <summary>
    /// Heuristic to mimic training or just testing the agent.
    /// </summary>
    /// <param name="actionsOut"></param>
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Mimic training or just testing Syntax
        var continiousActionsOut = actionsOut.ContinuousActions;
        continiousActionsOut[0] = Input.GetAxis("Vertical");
        continiousActionsOut[1] = Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// Collect the observations of Syntax.
    /// </summary>
    /// <param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_smartSyntaxController.Rb.transform);
    }

    /// <summary>
    /// Receive the actions from the agent/model.
    /// </summary>
    /// <param name="actions"></param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        _smartSyntaxController.Move(actions.ContinuousActions[0]);
        _smartSyntaxController.Rotate(actions.ContinuousActions[1]);

        AddReward(-2 / _maxSteps);
        Debug.Log($"Reward:{GetCumulativeReward()}");
    }


    /// <summary>
    /// Create visuals to see the progress of training.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_wallTag))
        {
            // Just visuals
            _trainingVisuals.SetFloorColor(Color.red);

            // Logic penalty for behavior
            AddReward(-1f);
            EndEpisode();
        }
        else if (collision.gameObject.CompareTag(_targetTag))
        {
            // Just visuals
            _trainingVisuals.SetFloorColor(Color.green);

            // Logic reinforce behavior
            AddReward(5f);
            EndEpisode();
        }
    }

}
