exports.up = (knex) => {
  return knex.schema.createTable('reservationhistoryreservations', (t) => {
    t.integer('reservationhistory_id')
      .references('id')
      .inTable('reservationhistory').notNull();
    t.integer('reservation_id')
      .references('id')
      .inTable('reservations').notNull();
  });
};

exports.down = (knex) => {
  return knex.schema.dropTable('reservationhistoryreservations');
};
