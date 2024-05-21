import { TestBed } from "@angular/core/testing";

import { OptimizationAnalyticsService } from "./optimization-analytics.service";

describe("OptimizationAnalyticsService", () => {
    let service: OptimizationAnalyticsService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(OptimizationAnalyticsService);
    });

    it("should be created", () => {
        expect(service).toBeTruthy();
    });
});
