from abc import ABC, abstractmethod
import asyncio

class IProblem(ABC):
    @abstractmethod
    async def get_value(self, x):
        pass