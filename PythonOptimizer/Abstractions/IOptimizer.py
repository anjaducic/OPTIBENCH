from abc import ABC, abstractmethod

class IOptimizer(ABC):
    @abstractmethod
    async def optimize(self, problem):
        pass
