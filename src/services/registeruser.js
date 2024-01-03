/* eslint-disable no-unused-vars */
const bcrypt = require('bcrypt-nodejs');
const ValidationError = require('../errors/validationError');

module.exports = (app) => {
  const getAll = (filter = {}) => {
    return app.db('registerusers').where(filter).select([
      'id',
      'username',
      'email',
    ]);
  };

  const find = (filter = {}) => {
    return app.db('registerusers').where(filter).first();
  };

  const getPasswordHash = (pass) => {
    const salt = bcrypt.genSaltSync(10);
    return bcrypt.hashSync(pass, salt);
  };

  const save = async (registeruser) => {
    if (!registeruser.username) throw new ValidationError('Username é um atributo obrigatório!');
    if (!registeruser.email) throw new ValidationError('Email é um atributo obrigatório!');
    if (!registeruser.password) throw new ValidationError('Password é um atributo obrigatório!');

    const existingUser = await app.services.registeruser.getByEmail(registeruser.email);

    const newRegisterUser = { ...registeruser };
    newRegisterUser.password = getPasswordHash(registeruser.password);

    if (existingUser) {
      return app.db('registerusers')
        .where('email', registeruser.email)
        .update(newRegisterUser, [
          'id',
          'username',
          'email',
        ]);
    }

    return app.db('registerusers').insert(newRegisterUser, [
      'id',
      'username',
      'email',
    ]);
  };

  const getByEmail = (email) => {
    return app.db('registerusers').where('email', email).first();
  };

  const update = (id, userRes) => {
    const newUpdateUserRegistration = { ...userRes };
    newUpdateUserRegistration.password = getPasswordHash(userRes.password);

    return app.db('registerusers')
      .where({ id })
      .update(newUpdateUserRegistration, [
        'id',
        'username',
        'email',
      ]);
  };

  const updatePassword = async (id, newPassword, confirmNewPassword) => {
    const user = await app.services.registeruser.find({ id });
    if (!user) {
      return { error: 'Utilizador não encontrado!' };
    }

    if (newPassword !== confirmNewPassword) {
      return { error: 'A Palavra Passe deve ser igual nos dois campos!' };
    }

    const updatePasswords = await Promise.all([
      app.db('registerusers').where({ id }).update({ password: newPassword }, '*'),
      app.db('users').where({ id }).update({ password: newPassword }, '*'),
    ]);

    return { success: true };
  };

  return {
    getAll,
    find,
    getByEmail,
    save,
    update,
    updatePassword,
  };
};
