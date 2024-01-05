/* eslint-disable no-unused-vars */
const request = require('supertest');
const jwt = require('jwt-simple');

const app = require('../../src/app');

const route = '/restaurantresponsibles';

const responsiblesecret = 'ipca!DWM@202324';

let responsible;
let responsibleRegistration;

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

  const res = await app.services.restaurantresponsible.save({
    flname: 'Rui Barreto',
    phone: 912345678,
    email: 'ruibarreto@gmail.com',
    password: '12345',
    image: null,
    restaurantregistration_id: responsible.id,
  });

  responsibleRegistration = { ...res[0] };
  responsibleRegistration.token = jwt.encode(responsibleRegistration, responsiblesecret);
});

test('Test #22 - Listar todos os perfis de responsáveis de restaurantes', () => {
  return request(app).get(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .then((res) => {
      expect(res.status).toBe(200);
    });
});

test('Test #23 - Listar um perfil de um responsável de um restaurante por ID', () => {
  return app.db('restaurantresponsibles')
    .insert({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      image: null,
      restaurantregistration_id: responsible.id,
    }, ['id'])
    .then((rresponsibleRes) => request(app).get(`${route}/${rresponsibleRes[0].id}`)
      .set('Authorization', `bearer ${responsibleRegistration.token}`))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.flname).toBe('Rui Barreto');
    });
});

test('Test #24 - Inserir um perfil de um responsável de um restaurante', async () => {
  const registrationRes = await request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      image: null,
      restaurantregistration_id: responsible.id,
    });

  expect(registrationRes.status).toBe(201);
  expect(registrationRes.body).not.toHaveProperty('password');
});

test('Test #25 - Inserir um perfil de um responsável de um restaurante sem nome', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      rphone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      image: null,
      restaurantregistration_id: responsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Nome do responsável do restaurante obrigatório!');
    });
});

test('Test #26 - Inserir um perfil de um responsável de um restaurante sem número de telemóvel', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      flname: 'Rui Barreto',
      email: 'ruibarreto@gmail.com',
      password: '12345',
      image: null,
      restaurantregistration_id: responsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Telemóvel do responsável do restaurante obrigatório!');
    });
});

test('Test #27 - Inserir um perfil de um responsável de um restaurante sem email', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      password: '12345',
      image: null,
      restaurantregistration_id: responsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Email do responsável do restaurante obrigatório!');
    });
});

test('Test #28 - Inserir um perfil de um responsável de um restaurante sem password', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${responsibleRegistration.token}`)
    .send({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      image: null,
      restaurantregistration_id: responsible.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Password do responsável do restaurante obrigatório!');
    });
});

test('Test #29 - Atualizar os dados de um perfil de um responsável de um restaurante', () => {
  return app.db('restaurantresponsibles')
    .insert({
      flname: 'Rui Barreto',
      phone: 912345678,
      email: 'ruibarreto@gmail.com',
      password: '12345',
      image: null,
      restaurantregistration_id: responsible.id,
    }, ['id'])
    .then((rresponsibleRes) => request(app).put(`${route}/${rresponsibleRes[0].id}`)
      .set('Authorization', `bearer ${responsibleRegistration.token}`)
      .send({
        flname: 'Rui Barreto',
        phone: 964769078,
        email: 'ruibarreto123@gmail.com',
        password: '54321',
        image: 'https://img.freepik.com/free-photo/portrait-handsome-man-with-dark-hairstyle-bristle-toothy-smile-dressed-white-sweatshirt-feels-very-glad-poses-indoor-pleased-european-guy-being-good-mood-smiles-positively-emotions-concept_273609-61405.jpg',
        restaurantregistration_id: responsible.id,
      }))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.email).toBe('ruibarreto123@gmail.com');
      expect(res.body).not.toHaveProperty('password');
    });
});
