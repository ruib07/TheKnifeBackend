const bcrypt = require('bcrypt-nodejs');

module.exports = (app) => {
  const getAll = (req, res, next) => {
    app.services.registeruser.getAll()
      .then((result) => res.status(200).json(result))
      .catch((err) => next(err));
  };

  const getId = (req, res, next) => {
    app.services.registeruser.find({
      id: req.params.id,
    })
      .then((result) => res.status(200).json(result))
      .catch((err) => next(err));
  };

  const create = (req, res, next) => {
    app.services.registeruser.save(req.body)
      .then((result) => {
        return res.status(201).json(result[0]);
      }).catch((err) => {
        next(err);
      });
  };

  const updatePassword = async (req, res) => {
    const { newPassword, confirmNewPassword } = req.body;

    const result = await app.services.registeruser.updatePassword(
      req.params.id,
      newPassword,
      confirmNewPassword,
    );

    if (result.error) return res.status(400).json(result);
    return res.status(200).json({ message: 'Palavra Passe atualizada com sucesso!' });
  };

  const update = (req, res, next) => {
    app.services.registeruser.update(req.params.id, req.body)
      .then((result) => res.status(200).json(result[0]))
      .catch((err) => {
        next(err);
      });
  };

  return {
    getAll,
    getId,
    create,
    update,
    updatePassword,
  };
};
