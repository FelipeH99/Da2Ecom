namespace WebAPI.Models.Write
{
    public abstract class ModelWrite<E, M> where E : class where M : ModelWrite<E, M>, new()
    {
        public static IEnumerable<E> ToEntity(IEnumerable<M> models) => models.Select(m => ToEntity(m));
        public static E ToEntity(M model) => (model == null) ? null : model.ToEntity();
        public abstract E ToEntity();
    }
}
