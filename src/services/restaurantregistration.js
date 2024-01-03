/* eslint-disable no-unused-vars */
const bcrypt = require('bcrypt-nodejs');
const ValidationError = require('../errors/validationError');

module.exports = (app) => {
  const getAll = (filter = {}) => {
    return app.db('restaurantregistrations').where(filter).select([
      'id',
      'flname',
      'phone',
      'email',
      'name',
      'category',
      'desc',
      'rphone',
      'location',
      'image',
      'numberoftables',
      'capacity',
      'openingdays',
      'openinghours',
      'closinghours',
    ]);
  };

  const find = (filter = {}) => {
    return app.db('restaurantregistrations').where(filter).first();
  };

  const getPasswordHash = (pass) => {
    const salt = bcrypt.genSaltSync(10);
    return bcrypt.hashSync(pass, salt);
  };

  const save = async (restaurantregistration) => {
    if (!restaurantregistration.flname) throw new ValidationError('Nome do responsável do restaurante obrigatório!');
    if (!restaurantregistration.phone) throw new ValidationError('Telemóvel do responsável do restaurante obrigatório!');
    if (!restaurantregistration.email) throw new ValidationError('Email do responsável do restaurante obrigatório!');
    if (!restaurantregistration.password) throw new ValidationError('Password do responsável do restaurante obrigatório!');
    if (!restaurantregistration.name) throw new ValidationError('Nome do restaurante é um atributo obrigatório!');
    if (!restaurantregistration.category) throw new ValidationError('Categoria do restaurante é um atributo obrigatório!');
    if (!restaurantregistration.desc) throw new ValidationError('Descrição do restaurante é um atributo obrigatório!');
    if (!restaurantregistration.rphone) throw new ValidationError('Telefone do restaurante é um atributo obrigatório!');
    if (!restaurantregistration.location) throw new ValidationError('Localização do restaurante é um atributo obrigatório!');
    if (!restaurantregistration.image) throw new ValidationError('Imagem do restaurante é um atributo obrigatório!');
    if (!restaurantregistration.numberoftables) throw new ValidationError('Número de mesas do restaurante é um atributo obrigatório!');
    if (!restaurantregistration.capacity) throw new ValidationError('Capacidade do restaurante é um atributo obrigatório!');
    if (!restaurantregistration.openingdays) throw new ValidationError('Dias de funcionamento são um atributo obrigatório!');
    if (!restaurantregistration.openinghours) throw new ValidationError('Horas de abertura são um atributo obrigatório!');
    if (!restaurantregistration.closinghours) throw new ValidationError('Horas de fecho são um atributo obrigatório!');

    const newRestaurantRegistration = { ...restaurantregistration };
    newRestaurantRegistration.password = getPasswordHash(restaurantregistration.password);

    return app.db('restaurantregistrations').insert(newRestaurantRegistration, [
      'id',
      'flname',
      'phone',
      'email',
      'name',
      'category',
      'desc',
      'rphone',
      'location',
      'image',
      'numberoftables',
      'capacity',
      'openingdays',
      'openinghours',
      'closinghours',
    ]);
  };

  const updatePassword = async (id, newPassword, confirmNewPassword) => {
    const restaurantResgistration = await app.services.restaurantregistration.find({ id });

    if (!restaurantResgistration) {
      return { error: 'Responsável não encontrado!' };
    }

    if (newPassword !== confirmNewPassword) {
      return { error: 'A Palavra Passe deve ser igual nos dois campos!' };
    }

    const updatePasswords = await Promise.all([
      app.db('restaurantregistrations').where({ id }).update({ password: newPassword }, '*'),
      app.db('restaurantresponsibles').where({ id }).update({ password: newPassword }, '*'),
    ]);

    return { success: true };
  };

  const update = (id, restaurantRes) => {
    const newUpdateRestaurantRegistration = { ...restaurantRes };
    newUpdateRestaurantRegistration.password = getPasswordHash(restaurantRes.password);

    return app.db('restaurantregistrations')
      .where({ id })
      .update(newUpdateRestaurantRegistration, [
        'id',
        'flname',
        'phone',
        'email',
        'name',
        'category',
        'desc',
        'rphone',
        'location',
        'image',
        'numberoftables',
        'capacity',
        'openingdays',
        'openinghours',
        'closinghours',
      ]);
  };

  return {
    getAll,
    find,
    save,
    updatePassword,
    update,
  };
};
