const { doesNotThrow } = require("assert");

Cypress.on("uncaught:exception", (err, runnable) => {
    // returning false here prevents Cypress from
    // failing the test
    return false;
});
before(function () {
    //something ...
    cy.visit("/#/datawarehouse", { timeout: 100000 });
});

it("Send all to the Data warehouse", () => {
    cy.wait(3000);
    cy.contains("Send All").click({ force: true });
    //cy.contains("Load from EMR").click({ force: true });

    cy.intercept("GET", "/api/DwhExtracts/**", (req) => {
        console.log(req);
    }).as("status");

    cy.wait("@status", {
        requestTimeout: 1000000,
        defaultCommandTimeout: 1000000,
    });
});
