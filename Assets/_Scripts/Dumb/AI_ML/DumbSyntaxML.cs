using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DumbSyntaxML : Agent
{
    [SerializeField] private string _wallTag = "Wall";
    [SerializeField] private string _targetTag = "Target";
    [SerializeField] private GameObject _target;
    [SerializeField] private TrainningVisuals _trainingVisuals;

    private float _maxSteps;
    private float _commulativeReward;
    private float _reward;
    private float _episodeCount;

    private DumbSyntaxController _dumbSyntaxController;

    void Start()
    {
        _dumbSyntaxController = GetComponent<DumbSyntaxController>();
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
        // Reset the position of Syntax to its original position!
        // Reset the position of the target to a random position!
        // Increase the episode count.
    }

    /// <summary>
    /// Reset the position of our target to something random.
    /// </summary>
    private void RaffleTicketSetRandomPosition()
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
        // Mimic training or just testing the agent

        // Get the continuousActions and store them in a VARiable.
        // Set values accordingly to the x and y inputs.
    }

    /// <summary>
    /// Collect the observations of the agent.
    /// </summary>
    /// <param name="sensor"></param>
    public override void CollectObservations(VectorSensor sensor)
    {
        // Give the model information about the position of Syntax!
    }

    /// <summary>
    /// Receive the actions from the agent/model.
    /// </summary>
    /// <param name="actions"></param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Use the values in actions to control the movement and rotation of Syntax.
        // Add a negative reward to tell Syntax to act fast!
        // print commulative reward divided by episode!
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

            // Give negative reward to Syntax because hitting a wall is bad!
            // End the episode.
        }
        else if (collision.gameObject.CompareTag(_targetTag))
        {
            // Just visuals
            _trainingVisuals.SetFloorColor(Color.green);

            // Logic reinforce behavior

            // Give a positive reward to Syntax, they got a raffle ticket!
            // End Episode.
        }
    }

}
