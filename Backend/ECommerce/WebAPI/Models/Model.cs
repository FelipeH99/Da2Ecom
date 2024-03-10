namespace WebAPI.Models
{
    public abstract class Model<E, M> where E : class where M : Model<E, M>, new()
    {
        public static IEnumerable<M> ToModel(IEnumerable<E> entities) => entities.Select(e => ToModel(e));
        public static M ToModel(E entity) => (entity == null) ? null : new M().SetModel(entity);
        public static IEnumerable<E> ToEntity(IEnumerable<M> models) => models.Select(m => ToEntity(m));
        public static E ToEntity(M model) => (model == null) ? null : model.ToEntity();
        public abstract E ToEntity();
        public abstract M SetModel(E entity);
    }
}
