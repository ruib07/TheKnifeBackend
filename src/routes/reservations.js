module.exports = (app) => {
    
    //Rota para o serviço getAll (Listar Reservas)
    const getAll = (req, res) => {
      app.services.reservation.getAll()
      .then((result) => res.status(200).json(result));
    };
  
    //Rota para o serviço find (Listar Reservas por ID)
    const getId = (req, res) => {
      app.services.reservation.find({ id: req.params.id })
      .then((result) => res.status(200).json(result));
    };
    
    //Rota para o serviço save (Criar uma Reserva)
    const create = async (req, res) => {
      const result = await app.services.reservation.save(req.body);
      if (result.error) return res.status(400).json(result);
      return res.status(201).json(result[0]);
    };
    
    //Rota para o serviço delReservation (Eliminar uma Reserva)
    const remove = (req, res) => {
      app.services.reservation.delReservation({ id: req.params.id })
      .then((result) => res.status(204).json(result));
    };
  
    //Rota para o serviço updateReservation (Modificar uma Reserva)
    const update = async (req, res, next) => {
      app.services.reservation.updateReservation({ id: req.params.id }, req.body)
      .then((result) => res.status(200).json(result[0]))
      .catch((err) => next(err));
    }
  
    return {
      getAll,
      getId,
      create,
      remove,
      update
    };
  };