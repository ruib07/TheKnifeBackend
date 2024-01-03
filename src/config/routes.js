module.exports = (app) => {
  app.route('/auths/responsiblesignin')
    .post(app.routes.auths.responsiblesignin);

  app.route('/auths/responsiblesignup')
    .post(app.routes.restaurantresponsibles.create);

  app.route('/restaurantregistrations')
    .get(app.routes.restaurantregistrations.getAll)
    .post(app.routes.restaurantregistrations.create);

  app.route('/restaurantregistrations/:id')
    .get(app.routes.restaurantregistrations.getId)
    .put(app.routes.restaurantregistrations.update);

  app.route('/restaurantregistrations/:id/updatepassword')
    .put(app.routes.restaurantregistrations.updatePassword);

  app.route('/restaurantresponsibles')
    .all(app.config.passport.responsibleauthenticate())
    .get(app.routes.restaurantresponsibles.getAll)
    .post(app.routes.restaurantresponsibles.create);

  app.route('/restaurantresponsibles/:id')
    .all(app.config.passport.responsibleauthenticate())
    .get(app.routes.restaurantresponsibles.getId)
    .put(app.routes.restaurantresponsibles.update);

  app.route('/restaurants')
    .get(app.routes.restaurants.getAll)
    .post(app.routes.restaurants.create);

  app.route('/restaurants/:id')
    .all(app.config.passport.responsibleauthenticate())
    .get(app.routes.restaurants.getId)
    .put(app.routes.restaurants.update);

  app.route('/registerusers')
    .get(app.routes.registerusers.getAll)
    .post(app.routes.registerusers.create);

  app.route('/registerusers/:id')
    .get(app.routes.registerusers.getId)
    .put(app.routes.registerusers.update);

  app.route('/registerusers/:id/updatepassword')
    .put(app.routes.registerusers.updatePassword);

  app.route('/registerusers/login')
    .post(app.routes.registerusers.login);

  app.route('/users')
    .get(app.routes.users.getAll)
    .post(app.routes.users.create);

  app.route('/users/:id')
    .get(app.routes.users.getId)
    .put(app.routes.users.update);

  app.route('/contacts')
    .get(app.routes.contacts.getAllContacts)
    .post(app.routes.contacts.createContact);

  app.route('/contacts/:id')
    .get(app.routes.contacts.getContactById);
};
