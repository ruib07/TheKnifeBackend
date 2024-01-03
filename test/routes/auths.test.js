const request = require('supertest');
const app = require('../../src/app');

const responsibleSigninRoute = '/auths/responsiblesignin';
const responsibleSignupRoute = '/auths/responsiblesignup';
const responsibleRoute = '/restaurantresponsibles';
const responsibleRouteById = '/restaurantresponsibles/:id';

let responsible;

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
    openinghours: '10:30',
    closinghours: '23:00',
  });
  responsible = { ...registrationRes[0] };
});

test('Test #62 - Receber token ao autenticar para os responsáveis', () => {
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

test('Test #63 - Tentativa de autenticação errada para os responsáveis', () => {
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

test('Test #64 - Aceder a rotas protegidas dos responsáveis #1', () => {
  return request(app).get(responsibleRoute)
    .then((res) => {
      expect(res.status).toBe(401);
    });
});

test('Test #65 - Aceder a rotas protegidas dos responsáveis #2', () => {
  return request(app).get(responsibleRouteById)
    .then((res) => {
      expect(res.status).toBe(401);
    });
});

test('Test #66 - Criar um Responsável', () => {
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
