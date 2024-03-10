namespace WebAPI.Models.Read
{
    public abstract class ModelRead<E, M> where E : class where M : ModelRead<E, M>, new()
    {
        public static M ToModel(E entity) => (entity == null) ? null : new M().SetModel(entity);
        public static IEnumerable<M> ToModel(IEnumerable<E> entities) => entities.Select(e => ToModel(e));
        public abstract M SetModel(E entity);
    }
}
