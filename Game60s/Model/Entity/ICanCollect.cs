namespace Game60s.Model
{
    internal interface ICanCollect
    {
        void TryGetThis(Resourse[] resourses);
        void GetThis(Resourse res);
        int CountResourse { get; set; }
        void IncrementResourse();
    }
}