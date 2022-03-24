const { doesNotThrow } = require("assert");

Cypress.on("uncaught:exception", (err, runnable) => {
    // returning false here prevents Cypress from
    // failing the test
    return false;
});
before(function () {
    //something ...
    cy.visit("/#/hts", { timeout: 100000 });
});


it("Load from the EMR HIV testing services data", () => {
    cy.wait(3000);
    cy.contains("Load from EMR").click({ force: true });

    cy.intercept("GET", "/api/Hts/status/**").as("status");
    let xhr = [];
    cy.wait("@status", { timeout: 240000 })
        .its("response.statusCode")
        .should("equal", 200);


    

    /* cy.wait("@status", {
        requestTimeout: 1000000,
        defaultCommandTimeout: 1000000,
    });*/
});
