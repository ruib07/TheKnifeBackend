/* eslint-disable camelcase */
module.exports = (app) => {

    //Função para consultar tudo na tabela 'reservations'
    const getAll = () => {
      return app.db('reservations');
    };
  
    //Função para encontrar um registo na tabela 'reservations'
    const find = (filter = {}) => {
      return app.db('reservations').where(filter).first();
    };
  
    //Função para adicionar um registo na tabela 'reservations'
    const save = async (reservation) => {
      try {
        if (!reservation.client_name) return { error: 'Primeiro e último nome é um atributo obrigatório!' };
        if (!reservation.phonenumber) return { error: 'Numero de telefone é um atributo obrigatório!' };
        if (!reservation.reservationdate) return { error: 'Data é um atributo obrigatório!' };
        if (!reservation.reservationtime) return { error: 'Hora é um atributo obrigatório!' };
        if (!reservation.numberpeople) return { error: 'Numero de pessoas é um atributo obrigatório!' };
        if (!reservation.restaurant_id) return { error: 'ID do restaurante é um atributo obrigatório!' };
        if (!reservation.user_id) return { error: 'ID do utilizador é um atributo obrigatório!' };
        return app.db('reservations').insert(reservation, '*');
  
      } catch (error) {
        return { error: 'Erro interno ao realizar a reserva!' };
      }
    };
  
    //Função para eliminar um registo na tabela 'reservations'
    const delReservation = (filter = {}) => {
      return app.db('reservations').where(filter).del();
    };
  
    //Função para modificar um registo na tabela 'reservations'
    const updateReservation = (filter = {}, user) => {
      return app.db('reservations').where(filter).update(user, '*');
    };
  
    return {
      getAll,
      find,
      save,
      delReservation,
      updateReservation
    };
  
  };
    