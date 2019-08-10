namespace Assets.Parsing.Nodes.Base
{
    public abstract class EvaluatedNode : BranchedNode
    {
        public EvaluatedNode() : base()
        {
            this.IsDisplayed = false;
        }

        public abstract bool IsConditionTrue();
    }
}
