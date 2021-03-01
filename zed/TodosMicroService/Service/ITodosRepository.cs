using System.Collections.Generic;

namespace Todos.Api.Services
{
    public interface ITodosRepository
    {
        List<Todos.Api.Models.Todo> GetAll();
        Todos.Api.Models.Todo Get(int Id);

        ProcessingResult Add(Todos.Api.Models.Todo todo);

        ProcessingResult Update(Todos.Api.Models.Todo todo);

        ProcessingResult Delete(int Id);
        
    }
}
