before(function () {
    //something ...
    cy.visit("/#/registry/HTS", { timeout: 100000 });
});

describe("HIV Testing Services  Registry", () => {
    it("Edit the url registry", () => {
        cy.xpath('//*[@id="url"]')
            .clear()
            .type("https://auth.kenyahmis.org:7773");
    });
    it("Verify the url registry", () => {
        cy.xpath(
            "/html/body/liveapp-root/div/div/div[2]/div[1]/liveapp-registry-manager/div/div/div[2]/liveapp-registry-config/div/form/div/div[5]/div[2]/button"
        ).click();
    });
    it("Save the url registry", () => {
        cy.xpath(
            "/html/body/liveapp-root/div/div/div[2]/div[1]/liveapp-registry-manager/div/div/div[2]/liveapp-registry-config/div/form/div/div[5]/div[1]/button"
        ).click();
    });
});
