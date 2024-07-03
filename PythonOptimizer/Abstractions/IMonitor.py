from abc import ABC, abstractmethod
from Dtos import OptimizationResultDto
from Implementations import RemoteProblem

class IMonitor(ABC):
    @abstractmethod
    async def save(self, result: OptimizationResultDto, problem: RemoteProblem):
        pass
