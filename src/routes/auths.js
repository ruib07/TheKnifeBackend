const jwt = require('jwt-simple');
const bcrypt = require('bcrypt-nodejs');

const responsibleSecret = 'ipca!DWM@202324';

module.exports = (app) => {
  const responsiblesignin = (req, res, next) => {
    app.services.restaurantresponsible.find({
      email: req.body.email,
    })
      .then((responsible) => {
        if (bcrypt.compareSync(req.body.password, responsible.password)) {
          const payload = {
            id: responsible.id,
            flname: responsible.flname,
            phone: responsible.phone,
            email: responsible.email,
            password: responsible.password,
            image: responsible.image,
            restaurantregistration_id: responsible.restaurantregistration_id,
          };
          const token = jwt.encode(payload, responsibleSecret);
          res.header('Authorization', `bearer ${token}`).status(200).json({ token });
        } else {
          res.status(400).json({ error: 'Autenticação inválida!' });
        }
      })
      .catch((err) => next(err));
  };

  return { responsiblesignin };
};
