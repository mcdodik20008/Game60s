namespace Game60s.Model
{
    public interface IBorderElement
    {
        DirectionType GetDirection();
        void SetDirection(DirectionType direction);
        bool NeedTurn();
        void SetBorderType(BorderType bT);
    }
}
