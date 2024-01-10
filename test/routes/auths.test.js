const request = require('supertest');
const app = require('../../src/app');

const responsibleSigninRoute = '/auths/responsiblesignin';
const responsibleSignupRoute = '/auths/responsiblesignup';
const responsibleRoute = '/restaurantresponsibles';
const responsibleRouteById = '/restaurantresponsibles/:id';

let responsible;

const userSigninRoute = '/auths/usersignin';
const userSignupRoute = '/auths/usersignup';
const userRoute = '/users';

let user;

beforeAll(async () => {
  const registrationRes = await app.services.restaurantregistration.save({
    flname: 'Rui Barreto',
    phone: 912345678,
    email: 'ruibarreto@gmail.com',
    password: '12345',
    name: 'La Gusto Italiano',
    category: 'Comida Italiana',
    desc: 'Restaurante de comida italiana situado em Braga',
    rphone: 253456789,
    location: 'Rua Gonçalo Sousa 285',
    image: '/Frontend/theknife-website/src/assets/logos/TheKnife-LogoDark.png',
    numberoftables: 10,
    capacity: 200,
    openingdays: 'Aberto de segunda a sábado',
    averageprice: 18.75,
    openinghours: '10:30',
    closinghours: '23:00',
  });
  responsible = { ...registrationRes[0] };

  const res = await app.services.registeruser.save({
    username: 'goncalosousa',
    email: 'goncalosousa@gmail.com',
    password: 'goncalo123',
  });
  user = { ...res[0] };
});

test('Test #63 - Receber token ao autenticar para os responsáveis', () => {
  return app.services.restaurantresponsible.save({
    flname: 'Rui Auth',
    phone: 912345678,
    email: 'ruibarreto@gmail.com',
    password: '12345',
    image: null,
    restaurantregistration_id: responsible.id,
  })
    .then(() => request(app).post(responsibleSigninRoute)
      .send({
        email: 'ruibarreto@gmail.com',
        password: '12345',
      }))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body).toHaveProperty('token');
    });
});

test('Test #64 - Tentativa de autenticação errada para os responsáveis', () => {
  return app.services.restaurantresponsible.save({
    flname: 'Rui Auth',
    phone: 912345678,
    email: 'ruibarreto@gmail.com',
    password: '12345',
    image: null,
    restaurantregistration_id: responsible.id,
  })
    .then(() => request(app).post(responsibleSigninRoute)
      .send({
        email: 'ruibarreto@gmail.com',
        password: '67890',
      }))
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Autenticação inválida!');
    });
});

test('Test #65 - Aceder a rotas protegidas dos responsáveis #1', () => {
  return request(app).get(responsibleRoute)
    .then((res) => {
      expect(res.status).toBe(401);
    });
});

test('Test #66 - Aceder a rotas protegidas dos responsáveis #2', () => {
  return request(app).get(responsibleRouteById)
    .then((res) => {
      expect(res.status).toBe(401);
    });
});

test('Test #67 - Criar um Responsável', () => {
  return request(app)
    .post(responsibleSignupRoute)
    .send({
      flname: 'Rui Signup',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      image: null,
      restaurantregistration_id: responsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(201);
      expect(res.body[0].flname).toBe('Rui Signup');
      expect(res.body[0]).toHaveProperty('email');
      expect(res.body[0]).not.toHaveProperty('password');
    });
});

test('Test #68 - Receber Token User', () => {
  return app.services.user.save({
    username: 'Goncalo Auth',
    email: 'goncalosousa@gmail.com',
    password: 'goncalo123',
    image: null,
    registeruser_id: user.id,
  })
    .then(() => request(app).post(userSigninRoute)
      .send({
        email: 'goncalosousa@gmail.com',
        password: 'goncalo123',
      }))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body).toHaveProperty('usertoken');
    });
});

test('Test #69 - Autenticação Errado User', () => {
  return app.services.user.save({
    username: 'Goncalo Auth',
    email: 'goncalosousa@gmail.com',
    password: 'goncalo123',
    image: null,
    registeruser_id: user.id,
  })
    .then(() => request(app).post(userSigninRoute)
      .send({
        email: 'goncalosousa@gmail.com',
        password: 'goncalo12',
      }))
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Autenticação invalida');
    });
});

test('Test #70 - Aceder a rotas protegidas users', () => {
  return request(app).get(userRoute)
    .then((res) => {
      expect(res.status).toBe(401);
    });
});

test('Test #71 - Criar um Utilizador', () => {
  return request(app)
    .post(userSignupRoute)
    .send({
      username: 'Goncalo Signup',
      email: 'goncalo123@gmail.com',
      password: '12345',
      image: null,
      registeruser_id: user.id,
    })
    .then((res) => {
      expect(res.status).toBe(201);
      expect(res.body.username).toBe('Goncalo Signup');
      expect(res.body).toHaveProperty('email');
      expect(res.body).not.toHaveProperty('password');
    });
});
