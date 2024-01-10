const request = require('supertest');
const jwt = require('jwt-simple');

const app = require('../../src/app');

const route = '/users';

const userSecret = 'ipcaDWM@202324';

let user;
let userToken;

beforeAll(async () => {
  const res = await app.services.registeruser.save({
    username: 'goncalosousa',
    email: 'goncalosousa@gmail.com',
    password: 'goncalo123',
  });
  user = { ...res[0] };

  const userRes = await app.services.user.save({
    username: 'goncalosousa',
    email: 'goncalosousa@gmail.com',
    password: 'goncalo123',
    image: null,
    registeruser_id: user.id,
  });

  userToken = { ...userRes };
  userToken.usertoken = jwt.encode(userToken, userSecret);
});

test('Test #56 - Listar todos os utilizadores', () => {
  return request(app).get(route)
    .set('Authorization', `bearer ${userToken.usertoken}`)
    .then((res) => {
      expect(res.status).toBe(200);
    });
});

test('Test #57 - Listar um user por ID', () => {
  return app.db('users')
    .insert({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
      image: null,
      registeruser_id: user.id,
    }, ['id'])
    .then((userRes) => request(app).get(`${route}/${userRes[0].id}`)
      .set('Authorization', `bearer ${userToken.usertoken}`))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.username).toBe('goncalosousa');
    });
});

test('Test #58 - Inserir um utilizador', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${userToken.usertoken}`)
    .send({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
      image: null,
      registeruser_id: user.id,
    })
    .then((res) => {
      expect(res.status).toBe(201);
      expect(res.body.username).toBe('goncalosousa');
      expect(res.body).not.toHaveProperty('password');
    });
});

test('Test #58.1 - Guardar password encriptada', async () => {
  const res = await request(app).post(route)
    .set('Authorization', `bearer ${userToken.usertoken}`)
    .send({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
      image: null,
      registeruser_id: user.id,
    });

  expect(res.status).toBe(201);

  const { id } = res.body;
  const userRegistrationDB = await app.services.user.find({ id });
  expect(userRegistrationDB.password).not.toBeUndefined();
  expect(userRegistrationDB.password).not.toBe('goncalo123');
});

test('Test #59 - Inserir um utilizador sem username', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${userToken.usertoken}`)
    .send({
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
      image: null,
      registeruser_id: user.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Username é um atributo obrigatório!');
    });
});

test('Test #60 - Inserir um utilizador sem email', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${userToken.usertoken}`)
    .send({
      username: 'goncalosousa',
      password: 'goncalo123',
      image: null,
      registeruser_id: user.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Email é um atributo obrigatório!');
    });
});

test('Test #61 - Inserir um utilizador sem password', () => {
  return request(app).post(route)
    .set('Authorization', `bearer ${userToken.usertoken}`)
    .send({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      image: null,
      registeruser_id: user.id,
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Password é um atributo obrigatório!');
    });
});

test('Test #62 - Atualizar os dados de um utilizador', () => {
  return app.db('users')
    .insert({
      username: 'goncalosousa',
      email: 'goncalosousa@gmail.com',
      password: 'goncalo123',
      image: null,
      registeruser_id: user.id,
    }, ['id'])
    .then((userRes) => request(app).put(`${route}/${userRes[0].id}`)
      .set('Authorization', `bearer ${userToken.usertoken}`)
      .send({
        username: 'goncalocoutinhosousa',
        email: 'goncalosousa123@gmail.com',
        password: 'goncalo321',
        image: null,
        registeruser_id: user.id,
      }))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.username).toBe('goncalocoutinhosousa');
      expect(res.body).not.toHaveProperty('password');
    });
});
