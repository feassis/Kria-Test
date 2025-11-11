using Moq;
using MongoDB.Driver;
using DBProcessor.Database;

namespace DBProcessor.Tests
{
    public class DatabaseWrapperTests
    {

        [Fact]
        public void Constructor_ShouldStoreConnectionString()
        {
            // Act
            var wrapper = new DatabaseWrapper(Secrets.Secrets.DB_PATH);

            // Assert
            var field = typeof(DatabaseWrapper)
                .GetField("DBAddress", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var value = field.GetValue(wrapper) as string;
            Assert.Equal(Secrets.Secrets.DB_PATH, value);
        }

        [Fact]
        public async Task ConnectRaw_ShouldHandleExceptionsGracefully()
        {
            // Arrange
            var wrapper = new DatabaseWrapper("mongodb://invalid:27017");

            // Act
            var exception = await Record.ExceptionAsync(() =>
                wrapper.ConnectRaw("FakeDB", "FakeCollection"));

            // Assert
            Assert.Null(exception); // método deve capturar internamente
        }

        private class TestEntity
        {
            public string Name { get; set; }
        }
    }
}
