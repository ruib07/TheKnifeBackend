const request = require('supertest');
const uuid = require('uuid');

const app = require('../../src/app');

const route = '/registerusers';

const generateUniqueEmail = () => `${uuid.v4()}@gmail.com`;

test('Test #47 - Listar Utilizadores', () => {
  return request(app).get(route)
    .then((res) => {
      expect(res.status).toBe(200);
    });
});

test('Test #48 - Listar um utilizador por ID', () => {
  const mail = generateUniqueEmail();

  return app.db('registerusers')
    .insert({
      username: 'goncalosousa',
      email: mail,
      password: 'Goncalo-12',
    }, ['id'])
    .then((userRes) => request(app).get(`${route}/${userRes[0].id}`))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body.username).toBe('goncalosousa');
    });
});

test('Test #49 - Atualizar dados de um utilizador', () => {
  const mail = generateUniqueEmail();

  return app.db('registerusers')
    .insert({
      username: 'goncalosousa',
      email: mail,
      password: 'Goncalo@12-AA',
    }, ['id'])
    .then((userRes) => request(app).put(`${route}/${userRes[0].id}`)
      .send({
        username: 'GoncaloCoutinho',
        email: 'goncalocoutinho@gmail.com',
        password: 'Goncalo@12-BB',
      }))
    .then((res) => {
      expect(res.status).toBe(200);
      expect(res.body).not.toHaveProperty('password');
    });
});

test('Test #50 - Inserir um registo de um utilizador', () => {
  const mail = generateUniqueEmail();

  return request(app).post(route)
    .send({
      username: 'goncalosousa',
      email: mail,
      password: 'Goncalo@12-BB',
    })
    .then((res) => {
      expect(res.status).toBe(201);
      expect(res.body).not.toHaveProperty('password');
    });
});

test('Test #50.1 - Guardar password encriptada', async () => {
  const mail = generateUniqueEmail();

  const res = await request(app).post(route)
    .send({
      username: 'goncalosousa',
      email: mail,
      password: 'Goncalo@12-BB',
    });

  expect(res.status).toBe(201);

  const { id } = res.body;
  const registerUsersDB = await app.services.registeruser.find({ id });
  expect(registerUsersDB.password).not.toBeUndefined();
  expect(registerUsersDB.password).not.toBe('Goncalo@12-BB');
});

describe('Validação de criar um registo de um utilizador', () => {
  const mail = generateUniqueEmail();

  const testTemplate = (newData, errorMessage) => {
    return request(app).post(route)
      .send({
        username: 'goncalosousa',
        email: mail,
        password: 'Goncalo@12-BB',
        ...newData,
      })
      .then((res) => {
        expect(res.status).toBe(400);
        expect(res.body.error).toBe(errorMessage);
      });
  };

  test('Test #51 - Inserir um utilizador sem username', () => testTemplate({ username: null }, 'Username é um atributo obrigatório!'));
  test('Test #52 - Inserir um utilizador sem email', () => testTemplate({ email: null }, 'Email é um atributo obrigatório!'));
  test('Test #53 - Inserir um utilizador sem password', () => testTemplate({ password: null }, 'Password é um atributo obrigatório!'));
});

test('Test #54 - Inserir e confirmar a Palavra Passe', async () => {
  const mail = generateUniqueEmail();

  const registrationResponse = await request(app).post(route)
    .send({
      username: 'goncalosousa',
      email: mail,
      password: 'Goncalo@12-BB',
    });

  expect(registrationResponse.status).toBe(201);

  const updatePasswordResponse = await request(app).put(`${route}/${registrationResponse.body.id}/updatepassword`)
    .send({
      newPassword: 'Goncalo@13-AA',
      confirmNewPassword: 'Goncalo@13-AA',
    });

  expect(updatePasswordResponse.status).toBe(200);
  expect(updatePasswordResponse.body.message).toBe('Palavra Passe atualizada com sucesso!');
});

test('Test #55 - Inserir e confirmar a Palavra Passe com palavras-passe diferentes', async () => {
  const mail = generateUniqueEmail();

  const registrationResponse = await request(app).post(route)
    .send({
      username: 'goncalosousa',
      email: mail,
      password: 'Goncalo@12-BB',
    });

  expect(registrationResponse.status).toBe(201);

  const updatePasswordResponse = await request(app).put(`${route}/${registrationResponse.body.id}/updatepassword`)
    .send({
      newPassword: 'Goncalo@13-AA',
      confirmNewPassword: 'Goncalo@14-AA',
    });

  expect(updatePasswordResponse.status).toBe(400);
});
