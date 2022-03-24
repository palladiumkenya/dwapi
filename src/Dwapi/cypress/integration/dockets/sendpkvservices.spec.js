const { doesNotThrow } = require("assert");

Cypress.on("uncaught:exception", (err, runnable) => {
    // returning false here prevents Cypress from
    // failing the test
    return false;
});
before(function () {
    //something ...
    cy.visit("/#/cbs", { timeout: 100000 });
});
it("Send all to the Data warehouse", () => {
    cy.wait(3000);
    cy.contains("Send Warehouse").click({ force: true });
    //cy.contains("Load from EMR").click({ force: true });

    cy.intercept("GET", "/api/Cbs/status/**", (req) => {
        console.log(req);
    }).as("status");

    cy.wait("@status", {
        requestTimeout: 10000,
        defaultCommandTimeout: 10000,
    })
        .its("response.statusCode")
        .should("equal", 200);
});
