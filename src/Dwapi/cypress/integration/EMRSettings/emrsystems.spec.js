before(function () {
    //something ...
    cy.visit("/#/emrconfig", { timeout: 100000 });
});

describe("Emr Settings configuration", () => {
    const host_address = Cypress.env("host_address");
    const host_port = Cypress.env("host_port");
    const host_user = Cypress.env("host_user");
    const host_password = Cypress.env("host_password");
    it("visit the emr config page", () => {
        cy.visit("/#/emrconfig", { timeout: 100000 });
    });

    it("Get list of emr", () => {
        cy.request("GET", "/api/EmrManager").then(function (response) {
            cy.log(response);
            expect(response.status).equal(200);
            expect(response.body).to.not.be.null;
            console.log(response);
            expect(response.body).to.be.a("array");
        });
    });

    it("Set the default emr to KenyaEMR", () => {
        cy.xpath(
            "/html/body/liveapp-root/div/div/div[2]/div[1]/liveapp-emr-settings/div/div[1]/div/div/p-datatable/div/div[2]/table/tbody/tr[3]/td[2]/span/p-splitbutton/div/button[2]"
        ).click();

        cy.xpath(
            "/html/body/liveapp-root/div/div/div[2]/div[1]/liveapp-emr-settings/div/div[1]/div/div/p-datatable/div/div[2]/table/tbody/tr[3]/td[2]/span/p-splitbutton/div/div/ul/li[1]/a/span[2]"
        ).click();
    });

    it("Set the Database settings", () => {
        cy.xpath(
            '//*[@id="ui-tabpanel-0"]/div/liveapp-db-protocol/div/p-datatable/div/div[1]/table/tbody/tr/td[4]/span/button'
        ).click();
        cy.xpath('//*[@id="host"]').clear().type(`${host_address}`);
        cy.xpath('//*[@id="port"]').clear().type(`${host_port}`);
        cy.xpath('//*[@id="username"]').clear().type(`${host_user}`);
        cy.xpath(
            "/html/body/liveapp-root/div/div/div[2]/div[1]/liveapp-emr-settings/div/div[2]/div[1]/p-tabview/div/div/p-tabpanel[1]/div/div/liveapp-db-protocol/p-dialog/div/div[2]/form/div/div[5]/div[2]/input"
        )
            .clear()
            .type(`${host_password}`);
        cy.xpath('//*[@id="databaseName"]').clear().type("openmrs");
        cy.xpath(
            '//*[@id="ui-tabpanel-0"]/div/liveapp-db-protocol/p-dialog/div/div[2]/form/div/div[8]/div[2]/button'
        ).click();

        cy.xpath(
            '//*[@id="ui-tabpanel-0"]/div/liveapp-db-protocol/p-dialog/div/div[2]/form/div/div[8]/div[1]/button'
        ).click();
    });

    it("set Openmrs End Point", () => {
        cy.wait(6000);

        cy.xpath(
            "/html/body/liveapp-root/div/div/div[2]/div[1]/liveapp-emr-settings/div/div[1]/div/div/p-datatable/div/div[2]/table/tbody/tr[3]/td[1]/span/span"
        ).click();

        cy.xpath('//*[@id="ui-tabpanel-1-label"]/span[1]').click();
        cy.xpath(
            '//*[@id="ui-tabpanel-1"]/div/liveapp-rest-protocol/div/p-datatable/div/div[1]/table/tbody/tr/td[2]/span/button'
        ).click();
        cy.xpath('//*[@id="url"]')
            .clear()
            .type(
                "http://prod.kenyahmis.org:8600/openmrs/ws/rest/v1/smartcard"
            );
        cy.xpath(
            '//*[@id="ui-tabpanel-1"]/div/liveapp-rest-protocol/p-dialog/div/div[2]/form/div/div[2]/div[1]/button'
        ).click();
    });
});
