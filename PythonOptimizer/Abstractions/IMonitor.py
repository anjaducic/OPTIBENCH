from abc import ABC, abstractmethod
from Dtos import OptimizationResultDto

class IMonitor(ABC):
    @abstractmethod
    async def save(self, result: OptimizationResultDto):
        pass
