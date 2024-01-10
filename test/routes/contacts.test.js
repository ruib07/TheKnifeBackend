const request = require('supertest');

const app = require('../../src/app');

const route = '/contacts';

test('Test #72 - Inserir dados de contacto', async () => {
  const contactData = {
    name: 'GoncaloCoutinho',
    email: 'goncalocoutinho@gmail.com',
    phoneNumber: 123456789,
    subject: 'Test subject',
    message: 'Test message',
  };

  const createResponse = await request(app)
    .post(route)
    .send(contactData);

  expect(createResponse.status).toBe(201);
  expect(createResponse.body).toHaveProperty('id');
  expect(createResponse.body.name).toBe(contactData.name);
  expect(createResponse.body.email).toBe(contactData.email);
  expect(createResponse.body.phoneNumber).toBe(contactData.phoneNumber);
  expect(createResponse.body.subject).toBe(contactData.subject);
  expect(createResponse.body.message).toBe(contactData.message);
});

test('Test #73- Inserir um contacto sem nome', () => {
  return request(app)
    .post(route)
    .send({
      email: 'goncalocoutinho@gmail.com',
      phoneNumber: 123456789,
      subject: 'Test subject',
      message: 'Test message',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Preencha todos os campos obrigatórios!');
    });
});

test('Test #74 - Inserir um contacto sem email', () => {
  return request(app)
    .post(route)
    .send({
      name: 'GoncaloCoutinho',
      phoneNumber: 123456789,
      subject: 'Test subject',
      message: 'Test message',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Preencha todos os campos obrigatórios!');
    });
});

test('Test #75 - Inserir um contacto sem número de telefone', () => {
  return request(app)
    .post(route)
    .send({
      name: 'GoncaloCoutinho',
      email: 'goncalocoutinho@gmail.com',
      subject: 'Test subject',
      message: 'Test message',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Preencha todos os campos obrigatórios!');
    });
});

test('Test #76 - Inserir um contacto sem assunto', () => {
  return request(app)
    .post(route)
    .send({
      name: 'GoncaloCoutinho',
      email: 'goncalocoutinho@gmail.com',
      phoneNumber: 123456789,
      message: 'Test message',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Preencha todos os campos obrigatórios!');
    });
});

test('Test #77 - Inserir um contacto sem mensagem', () => {
  return request(app)
    .post(route)
    .send({
      name: 'GoncaloCoutinho',
      email: 'goncalocoutinho@gmail.com',
      phoneNumber: 123456789,
      subject: 'Test subject',
    })
    .then((res) => {
      expect(res.status).toBe(400);
      expect(res.body.error).toBe('Preencha todos os campos obrigatórios!');
    });
});

test('Test #78 - Obter todos os contatos', async () => {
  const getAllResponse = await request(app).get(route);
  expect(getAllResponse.status).toBe(200);
  expect(Array.isArray(getAllResponse.body)).toBe(true);
  expect(getAllResponse.body.length).toBeGreaterThan(0);
  expect(getAllResponse.body[0]).toHaveProperty('id');
});

test('Test #79 - Obter contato por ID', async () => {
  const contactId = 1;
  const response = await request(app).get(`${route}/${contactId}`);
  expect(response.status).toBe(200);
  expect(response.body).toHaveProperty('id', contactId);
});
