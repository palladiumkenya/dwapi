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

it("Load from the EMR", () => {
    cy.wait(3000);
    cy.contains("Load from EMR").click({ force: true });

    cy.intercept("GET", "/api/DwhExtracts/status/**", (req) => {
        console.log(req);
    }).as("status");

    cy.wait("@status", { times: 45 });
});
