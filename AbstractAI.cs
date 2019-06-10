public abstract class AbstractAI<SensoryData, DecisionData, Apparatus>
{
	private Apparatus parts;	// Structure of scripts the AI manipulates to influence the system

	// Scan the environment and return the data that the AI uses to make its decisions
	public abstract SensoryData Scan();
	
	// Process AI inputs and return the data that describes the AI's decisions
	public abstract DecisionData Process(SensoryData inputs);
	
	// Use the AI's decision data to act on the AI's Apparatus
	public abstract void Act(DecisionData decisions, Apparatus apps);
	
	private void Update()
	{
		Act(Process(Scan()), parts);
	}
}
